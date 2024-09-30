using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using UnityEngine;

namespace Game.Scripts.GameEntities
{
    public class Laser : Projectile
    {
        [SerializeField] private float damageInterval = 0.5f;

        private float _damageTime;

        public override void EntityStart()
        {
            base.EntityStart();
            _damageTime = 0;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            HandleCollision(collision);
        }

        private void HandleCollision(Collider2D collision)
        {
            var entity = collision.gameObject.GetComponent<GameEntity>();
            if (entity != null)
            {
                _damageTime += Time.deltaTime;
                if (_damageTime >= damageInterval)
                {
                    OnHit(entity);
                    _damageTime = 0;
                }
            }
        }

        protected virtual void OnHit(GameEntity entity)
        {
            UpdateScore(entity);
            entity.DisposeEntity();
        }

        protected virtual void UpdateScore(GameEntity entity)
        {
            _signalService.Publish(new UpdateScoreSignal { Value = entity.DieScore });
        }
    }
}