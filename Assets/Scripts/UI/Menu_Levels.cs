using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Levels : MonoBehaviour
{

    public GameObject UI;
    public void OpenLevel(int levelId)
    {
        UI.SetActive(true);
        string levelName = "Level_00" + levelId;
        SceneManager.LoadScene(levelName);

    }
}
