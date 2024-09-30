using Asteroids.Game.Core;
using Asteroids.Game.Management;
using Asteroids.Game.Signals;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Services
{
    public class GameContainer : IGameContainer,
        IInitializable, ITickable, IFixedTickable, ILateDisposable
    {
        private GameState _currentGameState;

        private ISignalService _signalService;
        private IGameLoop _currentGame;
        private List<IGameState> _gameStates;

        private readonly Dictionary<GameState, System.Type> _stateMapping = new Dictionary<GameState, System.Type>()
        {
            {GameState.Loading, typeof(GameLoadState) },
            {GameState.Ready, typeof(GameReadyState) },
            {GameState.Running, typeof(GameRunningState) },
            {GameState.GameOver, typeof(GameOverState) },
        };

        [Inject]
        private void InitContainer(ISignalService signalService,
            IGameLoop gameLoop,
            List<IGameState> gameStates)
        {
            _signalService = signalService;
            _currentGame = gameLoop;
            _gameStates = gameStates;
        }

        public void Initialize()
        {
            _signalService.Subscribe<GameStateUpdateSignal>(SetGameState);
            _signalService.Subscribe<RemoveAllGameEntitiesSignal>(FlushGameEnities);
        }

        private void FlushGameEnities(RemoveAllGameEntitiesSignal signal)
        {
            _currentGame?.DisposeGameEntities();
        }

        private void SetGameState(GameStateUpdateSignal signal)
        {
            _currentGameState = signal.Value;

            var type = _stateMapping[_currentGameState];
            var state = _gameStates.Find(t => t.GetType().Equals(type));
            state.Execute();
        }

        public void Tick()
        {
            if (_currentGameState != GameState.Running)
                return;

            if (_currentGame != null)
                _currentGame.UpdateFrame();
        }

        public void FixedTick()
        {
            if (_currentGameState != GameState.Running)
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
            _signalService.RemoveSignal<RemoveAllGameEntitiesSignal>(FlushGameEnities);
        }
    }
}