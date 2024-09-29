using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using System;
using UnityEngine;

namespace Asteroids.Game.Management
{
    public class ContainerManager : MonoBehaviour
    {
        private IGame _game;
        private static ContainerManager _instance;
        public GameState _gameState;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        private void OnEnable()
        {
            SignalService.Subscribe<GameStateUpdateSignal>(SetGameState);
            SignalService.Subscribe<GameConfigLoadedSignal>(OnGameConfigLoaded);
        }


        private void OnGameConfigLoaded(GameConfigLoadedSignal signal)
        {
            _game = new GameLoop(signal.Value);
        }

        private void SetGameState(GameStateUpdateSignal signal)
        {
            _gameState = signal.Value;

            if(_gameState == GameState.GameOver)
            {
                _game.OnStateChanged(null);
            }
        }

        private void OnDisable()
        {
            SignalService.RemoveSignal<GameStateUpdateSignal>(SetGameState);
            SignalService.RemoveSignal<GameConfigLoadedSignal>(OnGameConfigLoaded);
        }

        public static void AddEntity(IGameEntity entity)
        {
            if (_instance == null)
            {
                Debug.Log("Cannot Add Entity as Container Instance is empty");
                return;
            }

            if (_instance._game == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            entity.EntityStart();

            _instance._game.AddGameEntity(entity);
        }

        public static void RemoveEntity(IGameEntity entity)
        {
            if (_instance == null)
            {
                Debug.Log("Cannot Add Entity as Container Instance is empty");
                return;
            }

            if (_instance._game == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            _instance._game.RemoveGameEntity(entity);
        }

        private void Update()
        {
            if (_gameState != GameState.Running)
                return;

            if (_instance._game != null)
                _instance._game.UpdateGame();
        }

        private void FixedUpdate()
        {
            if (_gameState != GameState.Running)
                return;

            if (_instance._game != null)
                _instance._game.OnFixedUpdate();
        }
    }
}