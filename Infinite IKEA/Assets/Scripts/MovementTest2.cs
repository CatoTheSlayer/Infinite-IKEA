using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
[SerializeField]    private float moveSpeed = 5f;
    private float verticalVelocity; // for jump physics

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
    private InputAction jumpAction;
    public bool canMove = true;
    public Animator animator;
    public AudioSource walk;

[SerializeField]    internal Camera playerCamera;
[SerializeField]    private float jumpForce = 5f;

    private Vector2 input;
    private Vector2 lookInput;
    public bool IsGrounded;
    private float Height = 1.5f; // Adjust based on your character's height


    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody>();

    }


    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");
 // Ensure physics handles gravity + rotation
        rigidBody.useGravity = true;
        rigidBody.freezeRotation = true;
        rigidBody.isKinematic = false; // required for MovePosition

        // Make movement unimpeded by drag / sleep
        rigidBody.linearDamping = 0f;
        rigidBody.angularDamping = 0f;
        rigidBody.sleepThreshold = 0f;
        rigidBody.WakeUp();
        //playerInput.ActivateInput();
        
        Time.timeScale = 1; // Ensure time scale is normal at start

        // Make sure Input System input is captured even if Send Messages isn't wired up.
        if (moveAction != null)
        {
            
            moveAction.Enable();
            moveAction.performed += move;
            //moveAction.canceled += ctx => input = Vector2.zero;
            Debug.Log ("Move action found and enabled.");
        }

        if (lookAction != null)
        {
            lookAction.Enable();
            lookAction.performed += Look;
            lookAction.canceled += ctx => lookInput = Vector2.zero;
            //lookAction.canceled += ctx => lookInput = Vector2.zero;
            Debug.Log ("Look action found and enabled.");

        }
        if (jumpAction != null)
        {
            jumpAction.Enable();
            jumpAction.performed += Jump;
            Debug.Log ("Jump action found and enabled.");
        }
    }
    void Update()
    {
        if (!moveAction.enabled)
        {
            Debug.LogWarning("Move action is not enabled!");
            moveAction.Enable();
        }
        if (lookAction != null)
        {
            if (!lookAction.enabled)
            {
                Debug.LogWarning("Look action is not enabled!");
                lookAction.Enable();
            }
        }
        if (jumpAction != null)
        {
            if (!jumpAction.enabled)
            {
                Debug.LogWarning("Jump action is not enabled!");
                jumpAction.Enable();
            }
        }


        HandleLook(Time.deltaTime);

        if (Physics.Raycast(transform.position, Vector3.down, Height))
        {
            IsGrounded = true;
            //Debug.Log("Grounded");
        }
        else
        {
            IsGrounded = false;
            //Debug.Log("Not Grounded!");
        }
    }

    public void move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        Debug.Log($"Move input: {input}");
    }
    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump input received");
        if (context.started && canMove && IsGrounded)
        {
            animator.SetTrigger("isJumping");
            // Apply jump by setting upward vertical velocity
            verticalVelocity = jumpForce;
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }   
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (moveAction != null)
            {
                input = moveAction.ReadValue<Vector2>();
            }
          
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);


            Vector3 moveDir = (forward * input.y) + (right * input.x);
            Vector3 displacement = moveDir * moveSpeed * Time.fixedDeltaTime;
            // Apply movement
            rigidBody.MovePosition(rigidBody.position + displacement);

            if (input.y != 0||input.x != 0)
            {
                animator.SetBool("isWalking", true);
                walk.Play();
            }
            else
            {
                animator.SetBool("isWalking", false);
                walk.Stop();
            }
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
}


