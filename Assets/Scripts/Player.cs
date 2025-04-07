using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityScale = 1f;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    
    [Header("Door Interaction")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private Transform interactionPoint;
    
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Camera mainCamera;
    private Vector3 horizontalVelocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        if (controller == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
        }
        
        if (groundCheck == null)
        {
            GameObject check = new GameObject("GroundCheck");
            check.transform.parent = transform;
            check.transform.localPosition = new Vector3(0, 0, 0);
            groundCheck = check.transform;
        }
        
        mainCamera = Camera.main;
        
        if (interactionPoint == null)
        {
            interactionPoint = mainCamera.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            horizontalVelocity = Vector3.zero;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        
        // Only apply movement when grounded
        if (isGrounded)
        {
            horizontalVelocity = move * moveSpeed;
            controller.Move(horizontalVelocity * Time.deltaTime);
        }
        else
        {
            // Apply stored horizontal velocity when in air
            controller.Move(horizontalVelocity * Time.deltaTime);
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        
        velocity.y += gravity * gravityScale * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        CheckDoorInteraction();
    }
    
    void CheckDoorInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(interactionPoint.position, interactionPoint.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    hit.collider.SendMessage("Active", SendMessageOptions.DontRequireReceiver);
                    Debug.Log("Door detected and activated!");
                }
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (interactionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(interactionPoint.position, interactionPoint.forward * interactionDistance);
        }
        
        // Add ground check visualization
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(groundCheck.position, Vector3.down * groundDistance);
        }
    }
}
