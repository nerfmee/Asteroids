using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Game.Core
{
    public class GameLoop : IGame
    {
        private List<IGameEntity> gameEntities;
        private Vector3 bottomLeftPoint;
        private Vector3 topRightPoint;

        public GameLoop()
        {
            gameEntities = new List<IGameEntity>();
            SetCameraBounds();
        }

        private void SetCameraBounds()
        {
            var cameraZ = Camera.main.transform.position.z;
            bottomLeftPoint = Camera.main.ScreenToWorldPoint(Vector3.zero - new Vector3(0, 0, cameraZ));
            topRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height) - new Vector3(0, 0, cameraZ));
        }

        public void AddGameEntity(IGameEntity gameEntity)
        {
            gameEntities.Add(gameEntity);
        }

        public void OnStateChanged(IGameState state)
        {
            state.Execute();
        }

        public void RemoveGameEntity(IGameEntity gameEntity)
        {
            if (gameEntities.Contains(gameEntity))
            {
                gameEntities.Remove(gameEntity);
            }
        }

        public void UpdateGame()
        {
            for (int i = 0; i < gameEntities?.Count; i++)
            {
                var entity = gameEntities[i];
                entity.UpdateEntity();
                HandleScreenWarp(entity.GameObject.transform, Vector3.zero);
            }
        }

        public void HandleScreenWarp(Transform target, Vector3 direction)
        {
            if (target != null)
            {
                var pos = target.position;

                if (pos.x < bottomLeftPoint.x - 0.3f)
                {
                    pos.x = topRightPoint.x + 0.3f;
                }
                else if (pos.x > topRightPoint.x + 0.3f)
                {
                    pos.x = bottomLeftPoint.x - 0.3f;
                }

                if (pos.y < bottomLeftPoint.y - 0.3f)
                {
                    pos.y = topRightPoint.y + 0.3f;
                }
                else if (pos.y > topRightPoint.y + 0.3f)
                {
                    pos.y = bottomLeftPoint.y - 0.3f;
                }

                target.position = pos;
            }
        }
    }
}