using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SocialPlatforms;

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
    internal bool canMove = true;
    public Camera playerCamera;
    private Vector2 input;
    private Vector2 lookInput;
    private Vector3 moveDirection;
    private float gravity = 9.81f;

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
    public void move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && canMove)
        {
            transform.Translate(Vector3.up * 5f);
        }
    }

    private void FixedUpdate()
    {
        if(canMove){
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = moveSpeed * input.y;
        float curSpeedY = moveSpeed * input.x;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        transform.localPosition = moveDirection * Time.deltaTime + transform.localPosition;
        }
    }
    public void HandleLook(float deltaTime)
    {
        if (canMove){
        yaw += lookInput.x * lookSensitivity;
        pitch -= lookInput.y * lookSensitivity;      // inverted vertical
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // body yaw
            transform.eulerAngles = new Vector2(0, yaw);

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


