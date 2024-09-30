using Asteroids.Game.Services;
using Asteroids.Game.Signals;
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
        public static GameState MockGameState { get; private set; }
        public int TotalLives { get; private set; }


        private ISignalService _signalService;
        private IPlayerProfileService _playerProfileService;

        [Inject]
        private void InitServices(ISignalService signalService,
            IPlayerProfileService playerProfileService)
        {
            _signalService = signalService;
            _playerProfileService = playerProfileService;
        }

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
            MockGameState = signal.Value;
        }

        private void AddScore(UpdateScoreSignal signal)
        {
            _playerProfileService.AddScore(signal.Value);
            _signalService.Publish(new DisplayScoreSignal { Value = _playerProfileService.GetScore() });
        }

        public void MockSetTotalLives(int count)
        {
            TotalLives = count;
        }

        private void PlayerDeathSignal(PlayerDiedSignal signal)
        {
            var lives = _playerProfileService.GetTotalLives();
            lives -= 1;
            if (lives <= 0)
            {
                lives = 0;
                _signalService.Publish(new GameStateUpdateSignal { Value = GameState.GameOver });
            }
            else
            {
                _signalService.Publish(new UpdatePlayerLivesSignal { Value = lives });
                _signalService.Publish<PlayerReviveSignal>();
            }
            _playerProfileService.SetTotalLives(lives);
        }

        private void OnGameConfigLoaded(GameConfigLoadedSignal signal)
        {
            TotalLives = signal.Value.TotalLives;
        }
    }
}