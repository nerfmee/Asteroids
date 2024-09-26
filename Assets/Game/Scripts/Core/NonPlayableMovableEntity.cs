using Asteroids.Game.Core;
using UnityEngine;

namespace Asteroids.Game.Core
{
    public abstract class NonPlayableMovableEntity : GameEntity
    {
        [SerializeField] private float speed = 4f;

        public override void Initialize()
        {
            SetDirection(Random.insideUnitCircle.normalized);
        }

        public override void UpdateEntity()
        {
            transform.position += speed * Time.deltaTime * MoveDirection;
        }
    }
}