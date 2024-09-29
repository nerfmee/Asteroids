using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using System.Collections;
using Game.Scripts.GameEntities;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class ShipMovement : GameEntity
    {
        [SerializeField] private float maxThrust = 10;
        [SerializeField] private float rotateSpeed = 135f;
        [SerializeField] private Rigidbody2D shipRigidbody2D;
        [SerializeField] private Collider2D shipCollider2D;
        [SerializeField] private SpriteRenderer renderer2D;
        private bool _isReviving;
        private BulletWeapon _bulletWeapon;
        private LaserWeapon _laserWeapon;

        public override void EntityStart()
        {
            base.EntityStart();

            _bulletWeapon = new BulletWeapon(transform, new Vector2(0, 0.6f), 0.5f);
            _laserWeapon = new LaserWeapon(transform, new Vector2(0, 0.6f), 1.0f);
            SetDirection(new Vector2(0, 1));
        }

        public override void EntityUpdate()
        {
            if (_isReviving)
                return;

            var horizontal = Input.GetAxis("Horizontal");

            if (horizontal != 0)
            {
                var sign = Mathf.Sign(horizontal) * -1f;
                transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed * sign);
                SetDirection(transform.TransformDirection(Vector3.up));
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                _bulletWeapon.TryShoot(MoveDirection);
            }
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                _laserWeapon.TryShoot(MoveDirection);
            }
        }

        public override void EntityFixedUpdate()
        {
            if (_isReviving)
                return;

            var addThrust = Input.GetAxis("Vertical") != 0;
            if (addThrust && shipRigidbody2D != null)
            {
                shipRigidbody2D.AddForce(MoveDirection * maxThrust, ForceMode2D.Force);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isReviving)
                return;

            SignalService.Publish<PlayerDiedSignal>();
            shipCollider2D.enabled = false;
            renderer2D.enabled = false;
            _isReviving = true;
        }

        private void OnEnable()
        {
            SignalService.Subscribe<PlayerReviveSignal>(OnPlayerShipRevived);
        }

        private void OnDisable()
        {
            SignalService.RemoveSignal<PlayerReviveSignal>(OnPlayerShipRevived);
        }

        private void OnPlayerShipRevived(PlayerReviveSignal signal)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            shipRigidbody2D.velocity = Vector3.zero;

            renderer2D.enabled = true;
            StartCoroutine(PlayReviveSequence());
        }

        private IEnumerator PlayReviveSequence()
        {
            for (int i = 0; i < 10; i++)
            {
                renderer2D.enabled = (i % 2 == 0);
                yield return new WaitForSeconds(0.1f);
            }
            renderer2D.enabled = true;
            yield return new WaitForSeconds(0.5f);
            _isReviving = false;
            shipCollider2D.enabled = true;
            SetDirection(new Vector3(0, 1));
        }
    }
}