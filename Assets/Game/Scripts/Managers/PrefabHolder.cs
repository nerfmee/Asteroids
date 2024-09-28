using Asteroids.Game.Config;
using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.Game.Management
{
    public class PrefabHolder : MonoBehaviour
    {
        public static PrefabHolder instance;

        private int _currentWave;
        private int _waveEnemiesCount;
        private float _timeStep;
        private GameConfig _config;

        [SerializeField] private AssetReference configReference;

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

        private void Update()
        {
            if (MainManager.CurrentGameState != GameState.Running)
                return;

            var wave = _config.EnemyWaves[_currentWave];
            if (Time.time - _timeStep > wave.Delay)
            {
                if (_waveEnemiesCount > wave.Count)
                {
                    _waveEnemiesCount = 0;
                    _currentWave++;

                    if (_currentWave > _config.EnemyWaves.Length - 1)
                        _currentWave = 0;

                    _timeStep = Time.time + 10f;
                }

                var degrees = Random.Range(0, 360f) * Mathf.Deg2Rad;
                var radius = Random.Range(10f, 15f);
                var pos = new Vector2(Mathf.Cos(degrees) * radius, Mathf.Sin(degrees) * radius);

                var entityId = wave.Enemies[Random.Range(0, wave.Enemies.Length)];
                InstantiateEntity(entityId, pos);

                _waveEnemiesCount += 1;

                _timeStep = Time.time;
            }
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