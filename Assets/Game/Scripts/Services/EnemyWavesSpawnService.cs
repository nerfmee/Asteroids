using Asteroids.Game.Config;
using Asteroids.Game.Core;
using Asteroids.Game.Services;
using Asteroids.Game.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Management
{
    public class EnemyWavesSpawnService : IInitializable
    {
        private GameConfig _gameConfig;
        private int _currentWave;
        private int _waveEnemiesCount;
        private float _timeStep;

        private IConfigCollectionService _configService;
        private ISignalService _signalService;
        private GameEntitySpawnService _spawnService;

        [Inject]
        private void Init(IConfigCollectionService facade,
            ISignalService signalService,
            GameEntitySpawnService spawnService)
        {
            _configService = facade;
            _signalService = signalService;
            _spawnService = spawnService;
        }

        public void Initialize()
        {
            _signalService.Subscribe<GameConfigLoadedSignal>(OnGameConfigLoaded);
        }

        public void OnUpdate()
        {
            if (_gameConfig == null)
            {
                _gameConfig = _configService.GameConfig;
                return;
            }

            var wave = _gameConfig.EnemyWaves[_currentWave];
            if (Time.time - _timeStep > wave.Delay)
            {
                if (_waveEnemiesCount > wave.Count)
                {
                    _waveEnemiesCount = 0;
                    _currentWave++;

                    if (_currentWave > _gameConfig.EnemyWaves.Length - 1)
                        _currentWave = 0;

                    _timeStep = Time.time + 10f;
                }

                var degrees = Random.Range(0, 360f) * Mathf.Deg2Rad;
                var radius = Random.Range(10f, 15f);
                var pos = new Vector2(Mathf.Cos(degrees) * radius, Mathf.Sin(degrees) * radius);

                var entityId = wave.Enemies[Random.Range(0, wave.Enemies.Length)];
                _spawnService.InstantiateEntity(entityId, pos);

                _waveEnemiesCount += 1;

                _timeStep = Time.time;
            }
        }

        public void Clear()
        {
            _currentWave = 0;
            _waveEnemiesCount = 0;
            _timeStep = 0;
        }

        private void OnGameConfigLoaded(GameConfigLoadedSignal signal)
        {
            _signalService.Publish(new GameStateUpdateSignal { Value = GameState.Ready });
        }
    }
}