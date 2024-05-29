using UnityEngine;

// Permet de conserver certains GameObjects d'une scene a une autre
public class Dont_Destroy_On_Load_Scene : MonoBehaviour
{
    public GameObject[] objects;
    void Awake()
    {
        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }
}