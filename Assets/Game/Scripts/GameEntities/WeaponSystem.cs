using Asteroids.Game.Management;
using UnityEngine;

namespace Game.Scripts.GameEntities
{
    public class WeaponSystem : IWeaponSystem
    {
        private BulletWeapon _bulletWeapon;
        private LaserWeapon _laserWeapon;

        public WeaponSystem(Transform playerTransform)
        {
            _bulletWeapon = new BulletWeapon(playerTransform, new Vector2(0, 0.6f), 0.5f);
            _laserWeapon = new LaserWeapon(playerTransform, new Vector2(0, 0.6f), 1.0f);
        }

        public void HandleShooting(GameEntitySpawnService spawnService, Vector2 moveDirection)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _bulletWeapon.TryShoot(spawnService, moveDirection);
            }
        
            if (Input.GetKeyDown(KeyCode.X))
            {
                _laserWeapon.TryShoot(spawnService, moveDirection);
            }
        }

        public float GetLaserCooldown()
        {
            return _laserWeapon.WeaponCooldown();
        }
    }
}