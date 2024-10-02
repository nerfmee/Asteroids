using UnityEngine;

namespace Game.Scripts.GameEntities
{
    public class ThrustSystem : IThrustSystem
    {
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;

        public ThrustSystem(Transform transform, Rigidbody2D rigidbody)
        {
            _transform = transform;
            _rigidbody = rigidbody;
        }

        public void HandleRotation()
        {
            var horizontal = Input.GetAxis("Horizontal");
            if (horizontal != 0)
            {
                var sign = Mathf.Sign(horizontal) * -1f;
                _transform.Rotate(Vector3.forward * Time.deltaTime * 135f * sign);
            }
        }

        public void ApplyThrust()
        {
            var addThrust = Input.GetAxis("Vertical") != 0;
            if (addThrust && _rigidbody != null)
            {
                _rigidbody.AddForce(_transform.up * 10, ForceMode2D.Force);
            }
        }
    }
}