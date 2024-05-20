using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Script uses with buttons from the Main Menu.
public class MainMenu : MonoBehaviour
{
    // When the Player clicks on the Play Button, it plays scene n°1
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    // When the Player clicks on the Quit Button, it closes the game
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
