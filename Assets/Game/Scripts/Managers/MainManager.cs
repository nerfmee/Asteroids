using Asteroids.Game.Signals;
using Asteroids.Game.UI;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Management
{
    public enum GameState
    {
        Loading,
        Ready,
        Running,
        GameOver
    }

    public class MainManager : MonoBehaviour
    {
        public static GameState CurrentGameState { get; private set; }

        [SerializeField] private GameState gameState;
        [SerializeField] private float newGameStartDelay = 2f;

        public int TotalLives { get; private set; }

        private int _currentScore = 0;
        
        private ISignalService _signalService;

        [Inject]
        private void InitSignalService(ISignalService signalService) => _signalService = signalService;

        private void OnEnable()
        {
            _signalService.Subscribe<GameConfigLoadedSignal>(OnGameConfigLoaded);
            _signalService.Subscribe<UpdateScoreSignal>(AddScore);
            _signalService.Subscribe<GameStateUpdateSignal>(SetGameState);
            _signalService.Subscribe<PlayerDiedSignal>(PlayerDeathSignal);
            
        }
        private void OnDisable()
        {
            _signalService.RemoveSignal<GameConfigLoadedSignal>(OnGameConfigLoaded);
            _signalService.RemoveSignal<UpdateScoreSignal>(AddScore);
            _signalService.RemoveSignal<GameStateUpdateSignal>(SetGameState);
            _signalService.RemoveSignal<PlayerDiedSignal>(PlayerDeathSignal);
        }

        public void SetGameState(GameStateUpdateSignal signal)
        {
            CurrentGameState = gameState = signal.Value;

            switch (gameState)
            {
                case GameState.Loading:
                    SetGameLoadState();
                    break;

                case GameState.Ready:
                    SetGameReadyState();
                    break;

                case GameState.Running:
                    StartGame();
                    break;

                case GameState.GameOver:
                    ShowGameOverScreen();
                    break;

                default:
                    break;
            }
        }

        private void SetGameLoadState()
        {
            var mainMenu = MenuManager.ShowMenu<MainMenuView>();
            MenuManager.HideMenu<GameplayView>();
            gameState = CurrentGameState = GameState.Loading;
            mainMenu.ToggleStartButton(false);
        }

        private void SetGameReadyState()
        {
            var mainMenu = MenuManager.ShowMenu<MainMenuView>();
            MenuManager.HideMenu<GameplayView>();
            gameState = CurrentGameState = GameState.Ready;

            mainMenu.ToggleStartButton(true);

            //PrefabHolder.instance.SpawnPlayerShip();
        }

        private void StartGame()
        {
            _currentScore = 0;
            TotalLives = 3;
            MenuManager.HideMenu<MainMenuView>();
            var menu = MenuManager.ShowMenu<GameplayView>();
            menu.DisplayScore(_currentScore);
            menu.SetTitle(string.Empty);
        }

        private void ShowGameOverScreen()
        {
            MenuManager.HideMenu<MainMenuView>();
            var menu = MenuManager.GetMenu<GameplayView>();
            menu.DisplayScore(_currentScore);
            menu.SetTitle("Gameover");
            menu.Clear();

            StartCoroutine(DelayedCall(newGameStartDelay));
        }

        private IEnumerator DelayedCall(float delay)
        {
            yield return new WaitForSeconds(delay);

            _signalService.Publish(new GameStateUpdateSignal { Value = GameState.Ready });
        }

        private void AddScore(UpdateScoreSignal signal)
        {
            _currentScore += signal.Value;
            _signalService.Publish(new DisplayScoreSignal { Value = _currentScore });
        }

        public void SetTotalLives(int count)
        {
            TotalLives = count;
        }

        private void PlayerDeathSignal(PlayerDiedSignal signal)
        {
            TotalLives -= 1;

            if (TotalLives <= 0)
            {
                TotalLives = 0;
                _signalService.Publish(new GameStateUpdateSignal { Value = GameState.GameOver });
            }
            else
            {
                _signalService.Publish(new UpdatePlayerLivesSignal { Value = TotalLives });
                _signalService.Publish<PlayerReviveSignal>();
            }
        }

        private void OnGameConfigLoaded(GameConfigLoadedSignal signal)
        {
            TotalLives = signal.Value.TotalLives;
        }
    }
}