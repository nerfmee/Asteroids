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

        public void TryShoot(Vector2 moveDirection)
        {
            if (!CanShoot()) 
                return;
            
            Shoot(moveDirection);
            _lastFireTime = Time.time;
        }

        private bool CanShoot()
        {
            return Time.time - _lastFireTime >= _fireRate;
        }

        protected abstract void Shoot(Vector2 moveDirection);
    }
}