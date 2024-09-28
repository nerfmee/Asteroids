using Asteroids.Game.Signals;
using Asteroids.Game.UI;
using System;
using System.Collections;
using UnityEngine;

public enum GameState
{
    Ready,
    Running,
    GameOver
}

public class MainManager : MonoBehaviour
{
    public static GameState CurrentGameState { get; private set; }

    [SerializeField] private GameState gameState;
    [SerializeField] private float newGameStartDelay = 2f;

    private int currentScore = 0;
    public int totalLives = 3;

    private void Start()
    {
        SetGameReadyState();
    }

    private void OnEnable()
    {
        SignalService.Subscribe<UpdateScoreSignal>(AddScore);
        SignalService.Subscribe<GameStateUpdateSignal>(SetGameState);
        SignalService.Subscribe<PlayerDiedSignal>(PlayerDeathSignal);
    }

    private void OnDisable()
    {
        SignalService.RemoveSignal<UpdateScoreSignal>(AddScore);
        SignalService.RemoveSignal<GameStateUpdateSignal>(SetGameState);
        SignalService.Subscribe<PlayerDiedSignal>(PlayerDeathSignal);

    }

    public void SetGameState(GameStateUpdateSignal signal)
    {
        CurrentGameState = gameState = signal.Value;

        switch (gameState)
        {
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

    private void SetGameReadyState()
    {
        MenuManager.ShowMenu<MainMenuView>();
        MenuManager.HideMenu<GameplayView>();
        gameState = CurrentGameState = GameState.Ready;

        PrefabHolder.instance.SpawnPlayerShip();
    }

    private void StartGame()
    {
        currentScore = 0;
        totalLives = 3;
        MenuManager.HideMenu<MainMenuView>();
        var menu = MenuManager.ShowMenu<GameplayView>();
        menu.DisplayScore(currentScore);
        menu.SetTitle(string.Empty);
    }

    private void ShowGameOverScreen()
    {
        MenuManager.HideMenu<MainMenuView>();
        var menu = MenuManager.GetMenu<GameplayView>();
        menu.DisplayScore(currentScore);
        menu.SetTitle("Gameover");
        menu.Clear();

        StartCoroutine(DelayedCall(newGameStartDelay));
    }

    private IEnumerator DelayedCall(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetGameReadyState();
    }

    private void AddScore(UpdateScoreSignal signal)
    {
        currentScore += signal.Value;
        SignalService.Publish(new DisplayScoreSignal { Value = currentScore });
    }

    private void PlayerDeathSignal(PlayerDiedSignal signal)
    {
        totalLives -= 1;

        if (totalLives <= 0)
        {
            totalLives = 0;
            SignalService.Publish(new GameStateUpdateSignal { Value = GameState.GameOver});
        }
        else
        {
            SignalService.Publish(new UpdatePlayerLivesSignal { Value = totalLives });
            SignalService.Publish<PlayerReviveSignal>();
        }

        
    }
}