using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

    private Animator animator; // Reference to the Animator
    private Rigidbody2D rb; // Reference to the Rigidbody2D
    public UIManager uiManager; // Reference to the UIManager
    public float playerSpeed; // Speed of the player

    void Start()
    {
        // Get the Animator and Rigidbody2D components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Idle to Running
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.Space))
        {
            // Show the running animation
            animator.SetInteger("State", 1);
            
            // Move the player to the right
            rb.linearVelocity = new Vector2(playerSpeed, rb.linearVelocity.y);
        }
        else if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D))
        {
            // Stop horizontal movement when the key is released
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            
            // Return to idle animation
            animator.SetInteger("State", 0);
        }
        
        // Idle to Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Show the jumping animation
            animator.SetInteger("State", 2);

            // Move the player up
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerSpeed);

            // Return to idle animation after a delay
            StartCoroutine(StateTransition(0.5f));
        }

        // Running to Jumping
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Input.GetKeyDown(KeyCode.Space))
        {
            // Show the jumping animation
            animator.SetInteger("State", 3);
            
            // Return to jumping animation after a delay
            StartCoroutine(StateTransition(0.5f, 4));
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
}
