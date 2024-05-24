using UnityEngine;

public class Dont_Destroy_On_Load_Scene : MonoBehaviour
{
    // Permet de declarer les elements a garder lors d'un changement de scenes
    public GameObject[] objects;
    void Awake()
    {
        foreach (var element in objects)
        {
            Debug.Log(element.name);
            DontDestroyOnLoad(element);
        }
    }
}