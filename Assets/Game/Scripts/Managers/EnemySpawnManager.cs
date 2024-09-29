using Asteroids.Game.Config;
using UnityEngine;

namespace Asteroids.Game.Management
{
    public class EnemySpawnManager
    {
        private GameConfig _gameConfig;
        private int _currentWave;
        private int _waveEnemiesCount;
        private float _timeStep;

        public EnemySpawnManager(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public void OnUpdate()
        {
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
                PrefabHolder.Instance.InstantiateEntity(entityId, pos);

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
    }
}