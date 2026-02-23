using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem; 

public class Camerascript : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 2f;

    private InputAction LookAction;
    private Vector2 lookInput = Vector2.zero;
    public float distanceToPlayer = 5f;
    float yRotation;
    float rotationY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        LookAction = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        lookInput += LookAction.ReadValue<Vector2>();
        something();
    }
    void FixedUpdate()
    {
        yRotation = Mathf.Clamp(yRotation - lookInput.y * 0.09f, -10f, 70f);
        transform.position = transform.parent.position - new Vector3(distanceToPlayer, yRotation, 0).normalized * distanceToPlayer;
        transform.LookAt(transform.parent.position);
        lookInput = Vector2.zero;
    }
    void something()
    {
        float mouseX = Input.GetAxis("Mouse X");
        rotationY += mouseX * mouseSensitivity * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
        
    }
}
