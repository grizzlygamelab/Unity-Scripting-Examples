using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float forwardSpeed = 5f;

    private Rigidbody2D rb;
    private Camera cam;
    private Vector2 movementInput;
    private Vector2 mousePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main; // Automatically finds the main camera in the scene
        
        // Hide the mouse cursor
        Cursor.visible = false;
    }

    void Update()
    {
        // Read movement input from WASD or arrow keys
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        // Get mouse position in screen coordinates
        mousePosition = Input.mousePosition;
    }

    void FixedUpdate()
    {
        HandleRotation();
        HandleMovement();
    }

    void HandleRotation()
    {
        // Convert mouse position from screen space to world space
        Vector2 mouseWorldPosition = cam.ScreenToWorldPoint(mousePosition);

        // Calculate the direction from the player to the mouse
        Vector2 directionToMouse = mouseWorldPosition - rb.position;

        // Calculate the angle in degrees
        // Mathf.Atan2 returns angle in radians, convert to degrees
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Apply rotation to the Rigidbody2D
        // The angle is for the Z-axis in Unity's Quaternion.Euler
        rb.MoveRotation(angle);
    }

    void HandleMovement()
    {
        // Check if the 'W' key or 'Up' arrow is pressed for forward movement
        bool isMovingForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        Vector2 velocity = Vector2.zero;

        if (isMovingForward)
        {
            // Move in the direction the object is currently facing
            // transform.right in 2D is the local 'forward' (right side of the sprite if it points right)
            velocity = (Vector2)transform.right * forwardSpeed;
        }
        else if (movementInput != Vector2.zero)
        {
            // Use WASD/Arrows for standard top-down movement
            velocity = movementInput.normalized * moveSpeed;
        }

        rb.linearVelocity = velocity;
    }
}
