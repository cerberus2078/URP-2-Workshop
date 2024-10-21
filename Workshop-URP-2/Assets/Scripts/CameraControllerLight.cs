using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5.0f;         // Speed of the camera movement
    public float rotationSpeed = 2.0f; // Sensitivity of the mouse look

    private float yaw = 0.0f;          // Y-axis rotation (horizontal)
    private float pitch = 0.0f;        // X-axis rotation (vertical)

    void Update()
    {
        HandleMovement();  // Move the camera using keyboard inputs (WASD)
        HandleRotation();  // Rotate the camera using the mouse
    }

    void HandleMovement()
    {
        // Get input for movement in horizontal and vertical directions
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow

        // Move the camera forward/backward and left/right based on input
        Vector3 movement = transform.forward * vertical + transform.right * horizontal;
        transform.position += movement * speed * Time.deltaTime;
    }

    void HandleRotation()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X"); // Horizontal mouse movement
        float mouseY = Input.GetAxis("Mouse Y"); // Vertical mouse movement

        // Adjust yaw (horizontal rotation) and pitch (vertical rotation) based on mouse input
        yaw += mouseX * rotationSpeed;
        pitch -= mouseY * rotationSpeed;

        // Clamp the pitch (vertical rotation) to prevent flipping
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Apply the rotation to the camera
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}