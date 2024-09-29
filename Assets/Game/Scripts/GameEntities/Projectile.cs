using Asteroids.Game.Core;
using UnityEngine;

namespace Game.Scripts.GameEntities
{
    public abstract class Projectile : GameEntity
    {
        [SerializeField] protected float speed = 4;
        [SerializeField] protected float timeToDestroy = 4f;

        private float _timestep;

        public override void EntityUpdate()
        {
            Move();
            HandleLifetime();
        }

        protected virtual void Move()
        {
            transform.position += speed * Time.deltaTime * MoveDirection;
        }

        private void HandleLifetime()
        {
            _timestep += Time.deltaTime;
            if (_timestep > timeToDestroy)
            {
                DisposeEntity();
            }
        }
    }
}