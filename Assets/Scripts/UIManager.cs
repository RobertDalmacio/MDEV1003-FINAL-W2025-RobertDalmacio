using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    private const int MAIN_MENU = 0; // Main menu scene index
    public GameObject PauseMenu; // Reference to the pause menu GameObject
    public GameObject GameOverMenu; // Reference to the game over menu GameObject
    public GameObject WinMenu; // Reference to the win menu GameObject

     private void Awake()
    {
        // Hide all menus
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        WinMenu.SetActive(false);

        // Ensure the game is not paused at the start
        ContinueGame();
    }
     public void PauseGame()
    {
        // Pause the game
        Time.timeScale = 0;

        // Show the pause menu
        PauseMenu.SetActive(true);
    }

    public void ContinueGame()
    {
        // Continue the game
        Time.timeScale = 1;

        // Hide the pause menu
        PauseMenu.SetActive(false);
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
    }

    public void GameWon()
    {
        // Pause the game
        Time.timeScale = 0;

        // Show the win menu
        WinMenu.SetActive(true);
    }
}
