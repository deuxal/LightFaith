using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu; // asigna el menú de pausa en el inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void LoadLevel(string levelName)
    {
        Debug.Log("Cargando nivel: " + levelName);
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    public void PauseGame()
    {
        Debug.Log("Pausando el juego");
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Debug.Log("Reanudando el juego");
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
