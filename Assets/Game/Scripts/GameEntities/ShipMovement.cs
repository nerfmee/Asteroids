using Asteroids.Game.Core;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class ShipMovement : GameEntity
    {
        [SerializeField] private float maxThrust = 10;
        [SerializeField] private float rotateSpeed = 135f;
        [SerializeField] private Rigidbody2D shipRigidbody2D;
        [SerializeField] private GameEntity bullet;
        [SerializeField] private float nextBulletSpawnTime = 1f;


        private bool _addThrust;
        public float currentTime;


        public override void Initialize()
        {
            base.Initialize();
        
            SetDirection(new Vector2(0, 1));
            currentTime = 0;
        }

        public override void UpdateEntity()
        {
            var horizontal = Input.GetAxis("Horizontal");

            if (horizontal != 0)
            {
                var sign = Mathf.Sign(horizontal) * -1f;
                transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed * sign);
                SetDirection(transform.TransformDirection(Vector3.up));
            }

            if (Input.GetKeyDown(KeyCode.Z) && Time.time - currentTime > nextBulletSpawnTime)
            {
                var position = transform.TransformPoint(new Vector2(0, 0.6f));
                var obj = Instantiate(bullet as GameEntity, position, Quaternion.identity);
                obj.SetDirection(MoveDirection);
                obj.SetVisibility(true);

                currentTime = Time.time;
            }
        }

        private void FixedUpdate()
        {
            var _addThrust = Input.GetAxis("Vertical") != 0;
            if (_addThrust && shipRigidbody2D != null)
            {
                shipRigidbody2D.AddForce(MoveDirection * maxThrust, ForceMode2D.Force);
            }
        }

        

        //public override void UpdateEntity()
        //{
            //var horizontal = Input.GetAxisRaw("Horizontal");
            //_addThrust = Input.GetAxisRaw("Vertical") != 0;

            //if (horizontal != 0)
            //{
            //    var sign = Mathf.Sign(horizontal) * -1f;
            //    transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed * sign);
            //    _moveDirection = transform.TransformDirection(Vector3.up);
            //}
        //}
    }
}