using UnityEngine;

namespace Asteroids.Game.Core
{
    public abstract class NonPlayableMovableEntity : GameEntity
    {
        [SerializeField] private float speed = 4f;

        public override void EntityStart()
        {
            base.EntityStart();
            SetDirection(Random.insideUnitCircle.normalized);
        }

        public override void EntityUpdate()
        {
            transform.position += speed * Time.deltaTime * MoveDirection;
        }

        public override void DisposeEntity()
        {
            base.DisposeEntity();
        }
    }
}