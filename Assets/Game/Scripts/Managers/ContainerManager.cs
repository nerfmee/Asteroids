using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Management
{
    public class ContainerManager : MonoBehaviour
    {
        private IGameLoop _game;
        private static ContainerManager _instance;
        public GameState _gameState;

        private ISignalService _signalService;

        [Inject]
        private void InitSignalService(ISignalService signalService) => _signalService = signalService;


        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        private void OnEnable()
        {
            _signalService.Subscribe<GameStateUpdateSignal>(SetGameState);
            _signalService.Subscribe<GameConfigLoadedSignal>(OnGameConfigLoaded);
        }


        private void OnGameConfigLoaded(GameConfigLoadedSignal signal)
        {
            //_game = new GameLoop(signal.Value);
        }

        private void SetGameState(GameStateUpdateSignal signal)
        {
            _gameState = signal.Value;

            if(_gameState == GameState.GameOver)
            {
                _game?.OnStateChanged(null);
            }
        }

        private void OnDisable()
        {
            _signalService.RemoveSignal<GameStateUpdateSignal>(SetGameState);
            _signalService.RemoveSignal<GameConfigLoadedSignal>(OnGameConfigLoaded);
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
                _instance._game.UpdateFrame();
        }

        private void FixedUpdate()
        {
            if (_gameState != GameState.Running)
                return;

            if (_instance._game != null)
                _instance._game.FixedUpdateFrame();
        }

        private void OnDestroy()
        {
            _game = null;
        }
    }
}