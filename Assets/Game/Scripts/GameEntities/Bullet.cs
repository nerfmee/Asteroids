using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using Game.Scripts.GameEntities;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class Bullet : Projectile
    {
        private bool _canUpdateScore;
        public override void EntityStart()
        {
            base.EntityStart();
            _canUpdateScore = gameObject.CompareTag("PlayerBullet");
        }

        protected override void Move()
        {
            transform.position += speed * Time.deltaTime * MoveDirection;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("Saucer"))
            {
                var entity = collision.gameObject.GetComponent<GameEntity>();
                if (entity != null)
                {
                    OnHit(entity);
                }
                DisposeEntity();
            }
        }
        
        protected virtual void OnHit(GameEntity entity)
        {
            if (_canUpdateScore)
            {
                _signalService.Publish(new UpdateScoreSignal { Value = entity.DieScore });
            }
            entity.DisposeEntity();
        }
    }
}