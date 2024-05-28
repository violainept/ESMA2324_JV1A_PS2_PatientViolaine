using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Levels : MonoBehaviour
{
    public void OpenLevel(int levelId)
    {
        string levelName = "Level_00" + levelId;
        SceneManager.LoadScene(levelName);
    }
}
