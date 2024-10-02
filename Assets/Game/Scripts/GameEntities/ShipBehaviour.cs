using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using Game.Scripts.GameEntities;
using UnityEngine;

public class ShipBehavior : GameEntity
{
    [SerializeField] private Rigidbody2D shipRigidbody2D;
    [SerializeField] private Collider2D shipCollider2D;
    [SerializeField] private SpriteRenderer renderer2D;

    private IWeaponSystem _weaponSystem;
    private IThrustSystem _thrustSystem;
    private IShipStatusUpdater _shipStatusUpdater;
    private IShipReviveHandler _shipReviveHandler;

    public override void EntityStart()
    {
        base.EntityStart();

        _weaponSystem = new WeaponSystem(transform);
        _thrustSystem = new ThrustSystem(transform, shipRigidbody2D);
        _shipStatusUpdater = new ShipStatusUpdater(transform, shipRigidbody2D, _weaponSystem, _signalService);
        _shipReviveHandler = new ShipReviveHandler(transform, shipRigidbody2D, shipCollider2D, renderer2D, _signalService);

        SetDirection(new Vector2(0, 1));
        _shipReviveHandler.Subscribe();
    }

    public override void EntityUpdate()
    {
        if (_shipReviveHandler.IsReviving) return;
        
        SetDirection(transform.TransformDirection(Vector3.up));
        _shipStatusUpdater.UpdateShipStatus();
        _thrustSystem.HandleRotation();
        _weaponSystem.HandleShooting(_spawnService, MoveDirection);
    }

    public override void EntityFixedUpdate()
    {
        if (_shipReviveHandler.IsReviving) return;

        _thrustSystem.ApplyThrust();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_shipReviveHandler.IsReviving) return;

        _signalService.Publish<PlayerDiedSignal>();
        shipCollider2D.enabled = false;
        renderer2D.enabled = false;
        _shipReviveHandler.StartRevive();
    }

    private void OnDisable()
    {
        _shipReviveHandler.Unsubscribe();
    }
}