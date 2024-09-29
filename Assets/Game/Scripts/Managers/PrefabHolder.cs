using Asteroids.Game.Config;
using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using System.Linq;
using UnityEngine;

namespace Asteroids.Game.Management
{
    public class PrefabHolder : MonoBehaviour
    {
        public static PrefabHolder instance;
        private GameConfig _config;

        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            SignalService.Subscribe<GameConfigLoadedSignal>(OnGameConfigLoaded);
        }

        private void OnDisable()
        {
            SignalService.RemoveSignal<GameConfigLoadedSignal>(OnGameConfigLoaded);
        }

        private void OnGameConfigLoaded(GameConfigLoadedSignal signal)
        {
            _config = signal.Value;
            SignalService.Publish(new GameStateUpdateSignal { Value = GameState.Ready });
        }

        public IGameEntity InstantiatePlayerBullet(Vector2 position)
        {
            return InstantiateEntity(_config.PlayerProjectile, position);
        }

        public IGameEntity InstantiateEnemyBullet(Vector3 position)
        {
            return InstantiateEntity(_config.EnemyProjectile, position);
        }

        public IGameEntity InstantiateEntity(string entityId, Vector2 position)
        {
            var gameplayElement = _config.GameElements.First(t => t.Id.Equals(entityId));
            var entity = InstantiateEntity(gameplayElement.Prefab, position);
            entity.OnInitialize(gameplayElement.Score);
            return entity;
        }

        public void SpawnPlayerShip()
        {
            Instantiate(_config.PlayerShip, Vector3.zero, Quaternion.identity);
        }

        public IGameEntity InstantiateEntity(IGameEntity entity, Vector2 position)
        {
            var obj = Instantiate(entity as GameEntity, position, Quaternion.identity);
            obj.SetVisibility(true);
            return obj;
        }
    }
}