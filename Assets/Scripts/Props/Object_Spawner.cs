using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire apparaitre l'objet au debut et si detruit

public class Object_Spawner : MonoBehaviour
{
    [Header("Prefab Object")]
    [SerializeField] private GameObject _Prefab;

    [Header("SpawnPosition")]
    [SerializeField] private Transform _Position1;
    [SerializeField] private Transform _Position2;

    private GameObject _SpawnObject;

    void Start()
    {
        StartCoroutine(SpawnProcess());
    }

    public void CoroutineStart()
    {
        StartCoroutine(SpawnProcess());
    }
    public void SpawnObject()
    {
        _SpawnObject = Instantiate(_Prefab, _Position1.position, Quaternion.identity);
    }

    private void MoveSpawnObject(Vector3 _Position)
    {
        _SpawnObject.transform.position = _Position;
    }

    IEnumerator SpawnProcess()
    {
        SpawnObject();
        yield return new WaitForSeconds(1f);
        _SpawnObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        MoveSpawnObject(_Position2.position);
        _SpawnObject.SetActive(true);
    }
}
