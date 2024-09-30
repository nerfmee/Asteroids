using Asteroids.Game.Core;
using Asteroids.Game.Management;
using Asteroids.Game.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Services
{
    public class GameContainer : IGameContainer,
        IInitializable, ITickable, IFixedTickable, ILateDisposable
    {
        private GameState _gameState;

        private ISignalService _signalService;
        private IGameLoop _currentGame;

        [Inject]
        private void Init(ISignalService signalService, IGameLoop gameLoop)
        {
            _signalService = signalService;
            _currentGame = gameLoop;
        }

        public void Initialize()
        {
            _signalService.Subscribe<GameStateUpdateSignal>(SetGameState);
        }

        private void SetGameState(GameStateUpdateSignal signal)
        {
            _gameState = signal.Value;

            if (_gameState == GameState.GameOver)
            {
                _currentGame?.OnStateChanged(null);
            }
        }

        public void Tick()
        {
            if (_gameState != GameState.Running)
                return;

            if (_currentGame != null)
                _currentGame.UpdateFrame();
        }

        public void FixedTick()
        {
            if (_gameState != GameState.Running)
                return;

            if (_currentGame != null)
                _currentGame.FixedUpdateFrame();
        }

        public void AddEntity(IGameEntity entity)
        {
            if (_currentGame == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            entity.EntityStart();

            _currentGame.AddGameEntity(entity);
        }

        public void RemoveEntity(IGameEntity entity)
        {
            if (_currentGame == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            _currentGame.RemoveGameEntity(entity);
        }

        public void LateDispose()
        {
            _signalService.RemoveSignal<GameStateUpdateSignal>(SetGameState);
        }
    }
}