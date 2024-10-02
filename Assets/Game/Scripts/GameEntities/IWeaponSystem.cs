using Asteroids.Game.Management;
using UnityEngine;

namespace Game.Scripts.GameEntities
{
    public interface IWeaponSystem
    {
        void HandleShooting(GameEntitySpawnService spawnService, Vector2 moveDirection);
        float GetLaserCooldown();
    }
}