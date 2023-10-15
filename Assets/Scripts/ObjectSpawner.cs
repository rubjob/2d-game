using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Object")]
    public GameObject Object;

    [Header("Constant")]
    public float Every = 1f;
    public bool SpawnOnStart = true;

    [Header("Event")]
    public UnityEvent OnSpawned;

    private bool IsEnable = false;

    private void Start() {
        if (SpawnOnStart) StartSpawning();
    }

    public void StartSpawning() {
        IsEnable = true;
        StartCoroutine(CoPeriodic(Every));
    }

    public void EndSpawning() {
        IsEnable = false;
    }

    private IEnumerator CoPeriodic(float every) {
        while (IsEnable) {
            Spawn();
            yield return new WaitForSeconds(every);
        }
    }

    public void Spawn() {
        GameObject spawnedObject = Instantiate(Object);
        spawnedObject.transform.position = transform.position;

        OnSpawned?.Invoke();
    }
}
