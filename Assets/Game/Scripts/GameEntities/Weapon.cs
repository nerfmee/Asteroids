using Asteroids.Game.Management;
using UnityEngine;

namespace Game.Scripts.GameEntities
{
    public abstract class Weapon
    {
        private float _lastFireTime;
        private readonly float _fireRate;
    
        protected readonly Transform PlayerTransform;
        protected Vector2 Offset;

        protected Weapon(Transform playerTransform, Vector2 offset, float fireRate)
        {
            PlayerTransform = playerTransform;
            Offset = offset;
            _fireRate = fireRate;
            _lastFireTime = 0f;
        }

        public void TryShoot(GameEntitySpawnService spawnService, Vector2 moveDirection)
        {
            if (!CanShoot()) 
                return;
            
            Shoot(spawnService, moveDirection);
            _lastFireTime = Time.time;
        }

        private bool CanShoot()
        {
            return Time.time - _lastFireTime >= _fireRate;
        }

        public float WeaponCooldown()
        {
            if (CanShoot())
            {
                float zeroCooldown = 0;
                return zeroCooldown;
            }
            
            float elapsed = Time.time - _lastFireTime;
            return Mathf.Clamp01((_fireRate - elapsed) / _fireRate);
        }

        protected abstract void Shoot(GameEntitySpawnService spawnService, Vector2 moveDirection);
    }
}