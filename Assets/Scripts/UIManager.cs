using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private const int MAIN_MENU = 0; // Main menu scene index
    public GameObject PauseMenu; // Reference to the pause menu GameObject
    public GameObject GameOverMenu; // Reference to the game over menu GameObject
    public GameObject WinMenu; // Reference to the win menu GameObject
    public GameObject JumpButton; // Reference to the jump button GameObject
    public GameObject RunButton; // Reference to the run button GameObject
    public GameObject PauseButton; // Reference to the pause button GameObject
    public Image MuteButtonImage; // Reference to the MuteButton's Image component
    public Sprite MuteSprite; // Sprite for the mute state
    public Sprite UnmuteSprite; // Sprite for the unmute state
    private AudioSource audioSource; // Reference to the AudioSource
    private bool isMuted = false; // Tracks mute state

    private void Awake()
    {
        // Hide all menus
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        WinMenu.SetActive(false);

        // Ensure the game is not paused at the start
        ContinueGame();

        // Get the AudioSource component
        audioSource = FindFirstObjectByType<AudioSource>();
    }
     public void PauseGame()
    {
        // Pause the game
        Time.timeScale = 0;

        // Show the pause menu
        PauseMenu.SetActive(true);

        // Hide the buttons
        HideButtons();
    }

    public void ContinueGame()
    {
        // Continue the game
        Time.timeScale = 1;

        // Hide the pause menu
        PauseMenu.SetActive(false);

        // Show the buttons
        ShowButtons();
    }

    public void GoToMainMenu()
    {
        // Load the main menu
        SceneManager.LoadScene(MAIN_MENU);
    }

    public void GameOver()
    {
        // Pause the game
        Time.timeScale = 0;

        // Show the game over menu
        GameOverMenu.SetActive(true);

        // Hide the buttons
        HideButtons();
    }

    public void GameWon()
    {
        // Pause the game
        Time.timeScale = 0;

        // Show the win menu
        WinMenu.SetActive(true);

        // Hide the buttons
        HideButtons();
    }

    public void RestartGame()
    {
        // Restart the game
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HideButtons()
    {
        // Hide the buttons
        JumpButton.SetActive(false);
        RunButton.SetActive(false);
        PauseButton.SetActive(false);
    }

    public void ShowButtons()
    {
        // Show the buttons
        JumpButton.SetActive(true);
        RunButton.SetActive(true);
        PauseButton.SetActive(true);
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
