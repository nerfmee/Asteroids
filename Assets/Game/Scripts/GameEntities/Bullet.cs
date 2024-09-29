using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class Bullet : GameEntity
    {
        [SerializeField] private float speed = 4;
        [SerializeField] private float timeToDestroy = 4f;

        private bool _canUpdateScore;
        private float _timestep;

        public override void EntityStart()
        {
            base.EntityStart();
            _canUpdateScore = gameObject.CompareTag("PlayerBullet");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("Saucer"))
            {
                var entity = collision.gameObject.GetComponent<GameEntity>();
                if (entity != null)
                {
                    if (_canUpdateScore)
                    {
                        SignalService.Publish(new UpdateScoreSignal { Value = entity.Score });
                    }
                    entity.DisposeEntity();
                }
                DisposeEntity();
            }
        }

        public override void EntityUpdate()
        {
            transform.position += speed * Time.deltaTime * MoveDirection;

            _timestep += Time.deltaTime;
            if (_timestep > timeToDestroy)
            {
                DisposeEntity();
                _timestep = 0;
            }
        }
    }
}