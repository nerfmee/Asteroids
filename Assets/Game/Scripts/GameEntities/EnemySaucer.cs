using Asteroids.Game.Core;
using Asteroids.Game.Management;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class EnemySaucer : NonPlayableMovableEntity
    {
        [SerializeField] private float shootDelay;
        [SerializeField] private float updateDirectionDelay;
        [SerializeField] private GameEntity bulletEntity;
        
        private float _timeStep;
        private float _directionTimeStep;

        public override void EntityUpdate()
        {
            base.EntityUpdate();

            if (Time.time - _timeStep > shootDelay)
            {
                GenerateProjectile();
                _timeStep = Time.time;
            }

            if (updateDirectionDelay > 0 && Time.time - _directionTimeStep > updateDirectionDelay)
            {
                SetDirection(Random.insideUnitCircle.normalized);
                _directionTimeStep = Time.time;
            }
        }

        private void GenerateProjectile()
        {
            var direction = Random.insideUnitCircle.normalized;
            var position = transform.position + (Vector3)direction;

            var obj = PrefabHolder.Instance.InstantiateEnemyBullet(position);
            obj.SetDirection(direction);
        }
    }
}