using Asteroids.Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
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
                    Debug.Log("Generate 2 medium");
                    break;
                case AsteroidSize.Medium:
                    Debug.Log("Generate 2 small");
                    break;
                case AsteroidSize.Small:
                    Debug.Log("Do Nothing man!!");
                    break;
                default:
                    break;
            }
        }
    }
}