using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Permet de rendre fonctionnel le Menu Principal
public class MainMenu : MonoBehaviour
{
    public GameObject UI;
    // Permet de lancer le jeu
    public void PlayGame()
    {
        UI.SetActive(true);
        SceneManager.LoadSceneAsync(1);
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
