using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Permet de changer de scene
public class Load_Specific_Scene : MonoBehaviour
{
    [Header("Parametres")]
    public string sceneName;
    private Animator fadeSystem;

    // Permet de charger un niveau
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(loadNextScene());
        }
    }
    public IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}