using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

    // Player Animator component
    Animator animator;

    void Start()
    {
        // Get the Animator component attached to the player
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("No Animator component found!");
        }
    }

    void Update()
    {
        // Idle to Running
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !Input.GetKey(KeyCode.Space))
        {
            animator.SetInteger("State", 1);
        }

        // Running to Idle
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.Space))
        {
            animator.SetInteger("State", 0);
        }
        
        // Idle to Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetInteger("State", 2);
            // Jumping to Idle
            StartCoroutine(StateTransition(0.5f));
        }

        // Running to Jumping
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetInteger("State", 3);
            // Jumping to Running
            StartCoroutine(StateTransition(0.5f, 4));
        }
    }

    // Change to next state after specified delay
    private IEnumerator StateTransition(float delay, int state = 0)
    {
        yield return new WaitForSeconds(delay);
        animator.SetInteger("State", state);
    }
}
