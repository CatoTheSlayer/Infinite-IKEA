using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

        [Header("look")]
[SerializeField]    private float lookSensitivity = 1f;
[SerializeField]    private Transform cameraTransform;           // drag your camera here
[SerializeField]    private float minPitch = -80f;
[SerializeField]    private float maxPitch = 80f;

    private float yaw;      // rotation around Y
    private float pitch;    // rotation around X
    private Rigidbody rigidBody;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    public bool canMove = true;
    public Camera playerCamera;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        rigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        // read look every frame, rotate body & camera
        HandleLook(Time.deltaTime);
    }
    void FixedUpdate()
    {
        if(canMove){
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0f, input.y);
        //rigidBody.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
        transform.position = (movement * moveSpeed * Time.deltaTime) + transform.position;
        }
    }
    private void HandleLook(float deltaTime)
    {
        if (canMove){
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        yaw += lookInput.x * lookSensitivity;
        pitch -= lookInput.y * lookSensitivity;      // inverted vertical
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // body yaw
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        // camera pitch – use a child/offset transform so it only affects X
        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }

    void Test()
    {
        Debug.Log("test");
    }
}



