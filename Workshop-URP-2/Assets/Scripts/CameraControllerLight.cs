using UnityEngine;

public class CameraControllerLight : MonoBehaviour
{
    public float rotationSpeed;
    public Transform playerBody;
    private float pitch = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleRotation();
    }

    void HandleRotation()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X"); // Horizontal mouse movement
        float mouseY = Input.GetAxis("Mouse Y"); // Vertical mouse movement

        // Rotate the player body around the Y-axis for yaw (left and right rotation)
        playerBody.Rotate(Vector3.up * mouseX * rotationSpeed);

        // Adjust pitch (vertical rotation) based on mouse input for up and down
        pitch -= mouseY * rotationSpeed;

        // Clamp the pitch (vertical rotation) to prevent flipping
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Apply the pitch rotation to the camera only (up and down)
        transform.localEulerAngles = new Vector3(pitch, 0.0f, 0.0f);
    }
}