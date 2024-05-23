using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Load_Specific_Scene : MonoBehaviour
{
    public string sceneName;
    private Animator fadeSystem;

    // Permet d'avoir un fondu
    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

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
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}