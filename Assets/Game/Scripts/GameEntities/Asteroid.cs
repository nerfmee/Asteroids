using Asteroids.Game.Core;
using Asteroids.Game.Management;
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
            switch (sizeType)
            {
                case AsteroidSize.Large:
                    for (int i = 0; i < 2; i++)
                    {
                        PrefabHolder.Instance.InstantiateEntity("ast_2", transform.position);
                    }

                    break;

                case AsteroidSize.Medium:
                    for (int i = 0; i < 2; i++)
                    {
                        PrefabHolder.Instance.InstantiateEntity("ast_3", transform.position);
                    }
                    break;

                case AsteroidSize.Small:

                    break;

                default:
                    break;
            }
        }
    }
}