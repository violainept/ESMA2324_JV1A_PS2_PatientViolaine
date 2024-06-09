using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//Permet d'activer ou desactiver l'objet et de changer sa position
public class Spawner_Object_Boss : MonoBehaviour
{
    public GameObject objectGO; // "objectGO" pour "objectGameObject"
    public GameObject spawner;
    public bool canDestroy = true;

    [SerializeField] private Transform _Position1;
    [SerializeField] private Transform _Position2;

    public void canDestroyObject()
    {
        canDestroy = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boss") && canDestroy)
        {
            StartCoroutine(SpawnProcess());
            canDestroy = false;
        }
    }

    // Fait apparaitre de nouveau l'objet

    public void SpawnObject()
    {
        spawner = Instantiate(objectGO, _Position1.position, Quaternion.identity);
    }

    private void MoveSpawnObject(Vector3 _Position)
    {
        spawner.transform.position = _Position;
    }

    IEnumerator SpawnProcess()
    {
        SpawnObject();
        yield return new WaitForSeconds(1f);
        objectGO.SetActive(false);
        yield return new WaitForSeconds(1f);
        MoveSpawnObject(_Position2.position);
        objectGO.SetActive(true);
    }
}
