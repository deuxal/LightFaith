using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Canvas uiCanvas;
    public Canvas gameOverCanvas;
    public Button resetButton;
    public Button backToMenuButton;
    public HealthSystemAttribute playerHealth;

    private void Start()
    {
        // Disable the game over canvas at the beginning
        gameOverCanvas.enabled = false;

        // Subscribe to the OnHealthModified event of the player's HealthSystemAttribute
        if (playerHealth != null)
        {
            playerHealth.OnHealthModified += CheckPlayerHealth;
        }
    }

    private void CheckPlayerHealth(int health)
    {
        if (health <= 0)
        {
            ShowGameOverPanel();
        }
    }

    public void ShowGameOverPanel()
    {
        // Disable the UI canvas
        uiCanvas.enabled = false;

        // Enable the game over canvas
        gameOverCanvas.enabled = true;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnHealthModified event when the game object is destroyed
        if (playerHealth != null)
        {
            playerHealth.OnHealthModified -= CheckPlayerHealth;
        }
    }
}