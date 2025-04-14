using UnityEngine;
using System.Collections;

public class IceSpikeController : MonoBehaviour
{
    public float fadeDuration = 1.5f; // Time it takes to fade out before destroying the object
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private bool isFading = false; // Flag to check if fading has started
    public UIManager uiManager; // Reference to the UIManager

    void Start()
    {
        // Get the SpriteRenderer and UIManager component
        spriteRenderer = GetComponent<SpriteRenderer>();
        uiManager = FindFirstObjectByType<UIManager>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the collided object is not the player
        if (!other.gameObject.CompareTag("Player") && !isFading)
        {
            // Start fading out the ice spike
            StartCoroutine(FadeOutAndDestroy());
        }
        // Check if the collided object is the player
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("GameOver!");
            // Show the Game Over menu
            uiManager.GameOver();
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        // Set the fading flag to true
        isFading = true;

        // Get the initial color of the sprite
        Color initialColor = spriteRenderer.color;

        // Gradually reduce the alpha value over time
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, Mathf.Lerp(1f, 0f, normalizedTime));
            yield return null;
        }

        // Ensure the sprite is fully transparent at the end
        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        // Destroy the ice spike after the fade-out
        Destroy(gameObject);
    }
}
