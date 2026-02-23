using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

[SerializeField] 
private float mouseSensitivity = 2f;

private float rotationY;
private CharacterController controller;
public float speed = 6.0f;
public float jumpHeight = 1.5f;
public float gravity = -9.81f;

private Vector3 velocity;
private bool isGrounded;

void Start()
{
controller = GetComponent<CharacterController>();
}

void Update()
{
rotation();
// Check if the player is grounded
isGrounded = controller.isGrounded;
if (isGrounded && velocity.y < 0)
{
velocity.y = 0f;
}

// Get input for movement
float moveX = Input.GetAxis("Horizontal");
float moveZ = Input.GetAxis("Vertical");
Vector3 move = transform.right * moveX + transform.forward * moveZ;

// Move the player
controller.Move(move * speed * Time.deltaTime);

// Jump logic
if (Input.GetButtonDown("Jump") && isGrounded)
{
velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
}

// Apply gravity
velocity.y += gravity * Time.deltaTime;

// Apply vertical movement
controller.Move(velocity * Time.deltaTime);
}
void rotation()
{
    float mouseX = Input.GetAxis("Mouse X");
    rotationY += mouseX * mouseSensitivity * Time.deltaTime;
    transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
    
}
}