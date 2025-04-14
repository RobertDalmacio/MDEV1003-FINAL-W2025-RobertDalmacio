using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Animator animator; // Reference to the Animator
    private Rigidbody2D rb; // Reference to the Rigidbody2D
    public UIManager uiManager; // Reference to the UIManager
    public float playerSpeed; // Speed of the player
    public Transform groundCheck; // Transform to check if the player is grounded
    public float groundCheckRadius = 0.2f; // Radius for ground check
    private bool isRunning = false; // Track if the player is running
    private bool isJumping = false; // Track if the player is jumping
    private bool isGrounded = false; // Track if the player is on the ground
    private bool isRunButtonPressed = false; // Track if the RunButton is pressed
    private bool isKeyboardRunning = false; // Track if the keyboard keys are pressed for running

    void Start()
    {
        // Get the Animator and Rigidbody2D components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Reset grounded state
        isGrounded = false;
        
        // List all colliders within the ground check radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);
        
        // Check if any of the colliders are tagged as "Platform"
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Platform"))
            {
                // If the player is touching a platform, set isGrounded to true
                isGrounded = true;
                break;
            }
        }

        // Handle running input from the keyboard
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            isKeyboardRunning = true;
        }
        else
        {
            isKeyboardRunning = false;
        }

        // Determine if the player should be running
        if (isKeyboardRunning || isRunButtonPressed)
        {
            OnRunButtonDown();
        }
        else
        {
            OnRunButtonUp();
        }

        // Handle jumping input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpButtonDown();
        }

        // Handle running
        if (isRunning && !isJumping)
        {
            // Show the running animation
            animator.SetInteger("State", 1);

            // Move the player to the right
            rb.linearVelocity = new Vector2(playerSpeed, rb.linearVelocity.y);
        }
        else if (!isRunning)
        {
            // Stop horizontal movement when running stops
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            // Return to idle animation
            animator.SetInteger("State", 0);
        }

        // Handle jumping
        if (isJumping && isGrounded)
        {
            // Show the jumping animation
            animator.SetInteger("State", 2);

            // Move the player up
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerSpeed);

            // Return to idle animation after a delay
            StartCoroutine(StateTransition(0.5f));
            
            // Reset jumping state
            isJumping = false;
        }
    }

    // Detect when the player lands on a platform
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Level Completed!");
            // Show the Game Won menu
            uiManager.GameWon();
        }
        if (collision.gameObject.CompareTag("GameOver"))
        {
            Debug.Log("GameOver!");
            // Show the Game Over menu
            uiManager.GameOver();
        }
    }

    // Change to next animator state after specified delay
    private IEnumerator StateTransition(float delay, int state = 0)
    {
        yield return new WaitForSeconds(delay);
        animator.SetInteger("State", state);
    }

    public void OnRunButtonDown()
    {
        isRunning = true;
    }

    public void OnRunButtonUp()
    {
        isRunning = false;
    }

    public void OnJumpButtonDown()
    {
        // Only allow jumping if the player is grounded
        if (isGrounded)
        {
            isJumping = true;
        }
    }

    public void RunButtonPressed()
    {
        isRunButtonPressed = true;
    }

    public void RunButtonReleased()
    {
        isRunButtonPressed = false;
    }
}
