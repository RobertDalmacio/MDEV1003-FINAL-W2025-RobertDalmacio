using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public Image MuteButtonImage; // Reference to the MuteButton's Image component
    public Sprite MuteSprite; // Sprite for the mute state
    public Sprite UnmuteSprite; // Sprite for the unmute state
    private AudioSource audioSource; // Reference to the AudioSource
    private bool isMuted = false; // Tracks mute state

    private void Awake()
    {
        // Get the AudioSource component
        audioSource = FindFirstObjectByType<AudioSource>();
    }

    public void PlayBtnClicked()
    {
        // Load Level 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToggleMute()
    {
        // Toggle mute state
        isMuted = !isMuted;

        if (audioSource != null)
        {
            audioSource.mute = isMuted;
        }

        // Update the button image
        if (MuteButtonImage != null)
        {
            MuteButtonImage.sprite = isMuted ? MuteSprite : UnmuteSprite;
        }
    }
}