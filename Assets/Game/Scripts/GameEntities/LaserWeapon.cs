using Asteroids.Game.Core;
using Asteroids.Game.Management;
using UnityEngine;

namespace Game.Scripts.GameEntities
{
    public class LaserWeapon : Weapon
    {
        public LaserWeapon(Transform playerTransform, Vector2 offset, float fireRate) : base(playerTransform, offset, fireRate)
        {
        }

        protected override void Shoot(GameEntitySpawnService spawnService, Vector2 moveDirection)
        {
            var position = PlayerTransform.TransformPoint(Offset);
            var laser = spawnService.InstantiatePlayerLaser(position);
            laser.SetDirection(moveDirection);
            RotateLaser(laser, moveDirection);
        }

        private void RotateLaser(IGameEntity gameEntity, Vector2 moveDirection)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            angle -= 90f;
            gameEntity.GameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}