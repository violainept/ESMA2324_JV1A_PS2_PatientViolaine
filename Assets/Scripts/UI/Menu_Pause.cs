using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// Permet de rendre fonctionnel le Menu pause
public class Menu_Pause : MonoBehaviour
{
    [Header("Parametres")]
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;
    public string sceneName;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Permet de relancer le jeu
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    // Permet de mettre sur pause le jeu
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Permet de revenir au Menu
    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
        GameIsPaused = false;
    }

    // Permet de quitter le jeu
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
