using Asteroids.Game.Core;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class Asteroid : NonPlayableMovableEntity
    {
        private enum AsteroidSize
        {
            Large,
            Medium,
            Small
        }

        [SerializeField] private AsteroidSize sizeType;
        [SerializeField] private string[] spawnOnDestroyIds;

        public override void DisposeEntity()
        {
            SplitEntity();
            base.DisposeEntity();
        }

        private void SplitEntity()
        {
            for (int i = 0; i < spawnOnDestroyIds?.Length; i++)
            {
                _spawnService.InstantiateEntity(spawnOnDestroyIds[i], transform.position);
            }
        }
    }
}