using Asteroids.Game.Management;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Game.Core
{
    public class GameLoop : IGameLoop
    {
        private EnemyWavesSpawnService _enemyWaveSpawnService;
        private List<IGameEntity> _gameEntities;
        private Vector3 _bottomLeftPoint;
        private Vector3 _topRightPoint;

        public GameLoop(EnemyWavesSpawnService enemySpawnService)
        {
            _gameEntities = new List<IGameEntity>();
            _enemyWaveSpawnService = enemySpawnService;
            SetCameraBounds();
        }

        private void SetCameraBounds()
        {
            var cameraZ = Camera.main.transform.position.z;
            _bottomLeftPoint = Camera.main.ScreenToWorldPoint(Vector3.zero - new Vector3(0, 0, cameraZ));
            _topRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height) - new Vector3(0, 0, cameraZ));
        }

        public void AddGameEntity(IGameEntity gameEntity)
        {
            _gameEntities.Add(gameEntity);
        }

        public void OnStateChanged(IGameState state)
        {
            state?.Execute();

            foreach (var item in _gameEntities)
            {
                GameObject.Destroy(item.GameObject);
            }
            _gameEntities.Clear();
            _enemyWaveSpawnService.Clear();
        }

        public void RemoveGameEntity(IGameEntity gameEntity)
        {
            if (_gameEntities.Contains(gameEntity))
            {
                _gameEntities.Remove(gameEntity);
            }
        }

        public void UpdateFrame()
        {
            _enemyWaveSpawnService.OnUpdate();

            for (int i = 0; i < _gameEntities?.Count; i++)
            {
                var entity = _gameEntities[i];
                entity.EntityUpdate();
                HandleScreenWarp(entity.GameObject.transform, Vector3.zero);
            }
        }

        public void FixedUpdateFrame()
        {
            for (int i = 0; i < _gameEntities?.Count; i++)
            {
                _gameEntities[i].EntityFixedUpdate();
            }
        }

        private void HandleScreenWarp(Transform target, Vector3 direction)
        {
            if (target != null)
            {
                var pos = target.position;
                var offset = target.localScale * 0.5f;

                if (pos.x < _bottomLeftPoint.x - offset.x)
                {
                    pos.x = _topRightPoint.x + offset.x;
                }
                else if (pos.x > _topRightPoint.x + offset.x)
                {
                    pos.x = _bottomLeftPoint.x - offset.x;
                }

                if (pos.y < _bottomLeftPoint.y - offset.y)
                {
                    pos.y = _topRightPoint.y + offset.y;
                }
                else if (pos.y > _topRightPoint.y + offset.y)
                {
                    pos.y = _bottomLeftPoint.y - offset.y;
                }

                target.position = pos;
            }
        }
    }
}