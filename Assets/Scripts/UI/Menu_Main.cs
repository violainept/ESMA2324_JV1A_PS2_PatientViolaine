using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Permet de rendre fonctionnel le Menu Principal
public class MainMenu : MonoBehaviour
{
    // Permet de lancer le jeu
    public void PlayGame()
    {
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
