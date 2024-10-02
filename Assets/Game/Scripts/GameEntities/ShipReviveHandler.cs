using System.Collections;
using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using UnityEngine;

namespace Game.Scripts.GameEntities
{
    public class ShipReviveHandler : IShipReviveHandler
    {
        private readonly Transform _shipTransform;
        private readonly Rigidbody2D _shipRigidbody;
        private readonly Collider2D _shipCollider;
        private readonly SpriteRenderer _shipRenderer;
        private readonly ISignalService _signalService;
        private bool _isReviving;
        public bool IsReviving
        {
            get => _isReviving;
            set => _isReviving = value;
        }

        private Coroutine _coroutine;

        public ShipReviveHandler(Transform shipTransform, Rigidbody2D shipRigidbody, Collider2D shipCollider, 
            SpriteRenderer shipRenderer, ISignalService signalService)
        {
            _shipTransform = shipTransform;
            _shipRigidbody = shipRigidbody;
            _shipCollider = shipCollider;
            _shipRenderer = shipRenderer;
            _signalService = signalService;
        }

        public void StartRevive()
        {
            _shipTransform.position = Vector3.zero;
            _shipTransform.rotation = Quaternion.identity;
            _shipRigidbody.velocity = Vector3.zero;

            _shipRenderer.enabled = true;
            _isReviving = true;
            _shipCollider.enabled = false;
            _coroutine = CoroutineRunner.Instance.StartCoroutine(PlayReviveSequence());
        }

        private IEnumerator PlayReviveSequence()
        {
            for (int i = 0; i < 10; i++)
            {
                if (_shipRenderer == null)
                {
                    yield break;
                }
                _shipRenderer.enabled = (i % 2 == 0);
                yield return new WaitForSeconds(0.1f);
            }

            _shipRenderer.enabled = true;
            yield return new WaitForSeconds(0.5f);
            _isReviving = false;
            _shipCollider.enabled = true;
        }

        public void Subscribe()
        {
            _signalService.Subscribe<PlayerReviveSignal>(OnPlayerShipRevived);
        }

        public void Unsubscribe()
        {
            if (_coroutine != null)
            {
                CoroutineRunner.Instance.StopCoroutine(_coroutine);
            }
            _signalService.RemoveSignal<PlayerReviveSignal>(OnPlayerShipRevived);
        }

        private void OnPlayerShipRevived(PlayerReviveSignal signal)
        {
            StartRevive();
        }
    }
}