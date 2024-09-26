using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float maxThrust = 10;
    [SerializeField] private float rotateSpeed = 135f;
    [SerializeField] private Rigidbody2D shipRigidbody2D;

    private bool _addThrust;
    private Vector2 _moveDirection;

    private void Start()
    {
        _moveDirection = new Vector2(0, 1);
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        _addThrust = Input.GetAxisRaw("Vertical") != 0;

        if (horizontal != 0)
        {
            var sign = Mathf.Sign(horizontal) * -1f;
            transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed * sign);
            _moveDirection = transform.TransformDirection(Vector3.up);
        }
    }

    private void FixedUpdate()
    {
        if (_addThrust && shipRigidbody2D != null)
        {
            shipRigidbody2D.AddForce(_moveDirection * maxThrust, ForceMode2D.Force);
        }
    }
}