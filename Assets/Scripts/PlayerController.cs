using UnityEngine;

/// <summary>
/// This class allows the Player to walk,run and jump. It moves in the direction of the Camera visual. The Camera visual is handled by the mouse position.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float rotationSpeed;

    public Transform cameraTransform;
    public float mouseSensitivity = 100f;
    public float minY = -40f;
    public float maxY = 80f;

    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundLayer;

    Rigidbody rigidBody;
    InputSystem_Actions inputActions;

    Vector2 moveInput;
    Vector2 lookInput;

    [SerializeField] bool jumpPressed;
    [SerializeField] bool runPressed;
    [SerializeField] bool isGrounded;

    float cameraPitch;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        inputActions = new InputSystem_Actions();
        Cursor.lockState=CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => jumpPressed = true;

        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;


        inputActions.Player.Sprint.performed += ctx => runPressed = true;
        inputActions.Player.Sprint.canceled += ctx => runPressed = false;
    }



    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        HandleCamera();
    }

    private void FixedUpdate()
    {
        CheckGround();
        if (!isGrounded)
        {
            runPressed = false;
        }

        HandleMovement();
    }
    private void HandleCamera()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, minY, maxY);

        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

    }
    private void CheckGround()
    {
        float rayLenght = 0.2f;

        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, rayLenght, groundLayer);

    }

    private void HandleMovement()
    {

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        Vector3 forward = Vector3.ProjectOnPlane(camForward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(camRight, Vector3.up).normalized;

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        float speed = 0f;

        speed = runPressed ? runSpeed : walkSpeed;

        Vector3 targetVelocity = moveDirection * speed;
        Vector3 currentVelocity = rigidBody.linearVelocity;

        Vector3 velocityChange = targetVelocity - new Vector3(currentVelocity.x, 0, currentVelocity.z);
        rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);


        if (moveDirection.magnitude >= 0.01f && isGrounded)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rigidBody.MoveRotation(
                Quaternion.Slerp(rigidBody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime)
            );
        }

        if (jumpPressed && isGrounded)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPressed = false;
        }

    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.2f);
    }
#endif


}
