using Asteroids.Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class EnemySaucer : NonPlayableMovableEntity
    {
        private enum SaucerType
        {
            Big,
            Small
        }

        [SerializeField] private float shootDelay;
        [SerializeField] private float updateDirectionDelay;
        [SerializeField] private GameEntity bulletEntity;
        private float timeStep;
        private float directionTimeStep;


        public override void UpdateEntity()
        {
            base.UpdateEntity();

            if (Time.time - timeStep > shootDelay)
            {
                GenerateProjectile();
                timeStep = Time.time;
            }

            if (updateDirectionDelay > 0 && Time.time - directionTimeStep > updateDirectionDelay)
            {
                SetDirection(Random.insideUnitCircle.normalized);
                directionTimeStep = Time.time;
            }
        }

        private void GenerateProjectile()
        {
            var direction = Random.insideUnitCircle.normalized;
            var position = transform.position + (Vector3)direction;
            var obj = Instantiate(bulletEntity as GameEntity, position, Quaternion.identity);
            obj.SetVisibility(true);
            obj.SetDirection(direction);
        }
    }
}