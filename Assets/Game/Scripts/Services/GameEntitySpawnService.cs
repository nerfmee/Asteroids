using Asteroids.Game.Config;
using Asteroids.Game.Core;
using Asteroids.Game.Services;
using Asteroids.Game.Signals;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Management
{
    public class GameEntitySpawnService : IInitializable
    {
        private GameConfig _gameConfig;

        private IConfigCollectionService _configService;
        private GameEntity.Factory _factory;
        private ISignalService _signalService;

        [Inject]
        private void Init(GameEntity.Factory factory,
            IConfigCollectionService facade,
            ISignalService signalService)
        {
            _configService = facade;
            _factory = factory;
            _signalService = signalService;
        }

        public void Initialize()
        {
            _signalService.Subscribe<GameStateUpdateSignal>(OnGameStateUpdate);
        }

        public IGameEntity InstantiateEnemyBullet(Vector3 position)
        {
            return InstantiateEntity(_gameConfig.EnemyProjectile, position);
        }

        public IGameEntity InstantiatePlayerBullet(Vector3 position)
        {
            return InstantiateEntity(_gameConfig.PlayerProjectile, position);
        }

        public IGameEntity InstantiatePlayerLaser(Vector3 position)
        {
            return InstantiateEntity(_gameConfig.PlayerLaser, position);
        }

        public IGameEntity InstantiateEntity(string entityId, Vector2 position)
        {
            var gameplayElement = _gameConfig.GameElements.First(t => t.Id.Equals(entityId));
            var gameEntity = _factory.Create(gameplayElement.Prefab);
            gameEntity.gameObject.transform.position = position;
            gameEntity.OnInitialize(gameplayElement.Score);
            gameEntity.SetVisibility(true);
            return gameEntity;
        }

        public IGameEntity InstantiateEntity(IGameEntity prefab, Vector2 pos)
        {
            var gameEntity = _factory.Create(prefab.GameObject);
            gameEntity.gameObject.transform.position = pos;
            gameEntity.SetVisibility(true);
            return gameEntity;
        }

        private void OnGameStateUpdate(GameStateUpdateSignal signal)
        {
            if (signal.Value == GameState.Ready)
                _factory.Create(_configService.GameConfig.PlayerShip);

            _gameConfig = _configService.GameConfig;
        }
    }
}