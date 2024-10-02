using Asteroids.Game.Signals;
using UnityEngine;

namespace Game.Scripts.GameEntities
{
    public class ShipStatusUpdater : IShipStatusUpdater
    {
        private Transform _shipTransform;
        private Rigidbody2D _rigidbody2D;
        private IWeaponSystem _weaponSystem;
        private ISignalService _signalService;

        public ShipStatusUpdater(Transform shipTransform, Rigidbody2D rigidbody, IWeaponSystem weaponSystem, ISignalService signalService)
        {
            _shipTransform = shipTransform;
            _rigidbody2D = rigidbody;
            _weaponSystem = weaponSystem;
            _signalService = signalService;
        }

        public void UpdateShipStatus()
        {
            _signalService.Publish(new UpdatePlayerPositionSignal { Value = _shipTransform.position });
            _signalService.Publish(new UpdatePlayerRotationSignal { Value = _shipTransform.eulerAngles.z });
            _signalService.Publish(new UpdatePlayerSpeedSignal { Value = _rigidbody2D.velocity.magnitude });
            _signalService.Publish(new UpdatePlayerLaserCooldownSignal { Value = _weaponSystem.GetLaserCooldown() });
        }
    }
}