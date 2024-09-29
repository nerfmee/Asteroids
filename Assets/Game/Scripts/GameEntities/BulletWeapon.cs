using Asteroids.Game.Management;
using UnityEngine;

namespace Game.Scripts.GameEntities
{
    public class BulletWeapon : Weapon
    {
        public BulletWeapon(Transform playerTransform, Vector2 offset, float fireRate) 
            : base(playerTransform, offset, fireRate)
        {
        }

        protected override void Shoot(Vector2 moveDirection)
        {
            var position = PlayerTransform.TransformPoint(Offset);
            var bullet = PrefabHolder.Instance.InstantiatePlayerBullet(position);
            bullet.SetDirection(moveDirection);
        }
    }
}