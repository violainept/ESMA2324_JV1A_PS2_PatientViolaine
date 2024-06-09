using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Permet de changer de scene
public class Load_Specific_Scene : MonoBehaviour
{
    [Header("Parametres")]
    public string sceneName;
    private GameObject transitionSystem;
    private Animator anim;

    private void Start()
    {
        transitionSystem = GameObject.FindGameObjectWithTag("Transition");
        anim = transitionSystem.GetComponent<Animator>();
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
        anim.SetTrigger("Transition");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        anim.SetTrigger("EndTransition");
    }
}