using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 15.0f;
    [SerializeField] private float rotationSpeed = 360.0f;
    [SerializeField] private Rigidbody bodyRef;
    [SerializeField] private Vector3 movementVector;

    private void Update()
    {
        movementVector = new Vector3(0, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        Vector3 direction = new(0, 0, movementVector.z * movementSpeed * Time.fixedDeltaTime * 100f);
        direction = bodyRef.rotation * direction;
        bodyRef.linearVelocity = direction;

        transform.Rotate(0, movementVector.y * rotationSpeed * Time.fixedDeltaTime, 0);
    }

    public void ResetPlayerPosition()
    {
        bodyRef.transform.position = new Vector3(0, 0, 6);
    }
}
