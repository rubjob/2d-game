using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HitboxTriggerer : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent<GameObject> OnObjectTriggered, OnObjectTriggering;

    private readonly HashSet<GameObject> triggeringObjects = new();

    public GameObject[] TriggeringObjects
    {
        get { return triggeringObjects.ToArray(); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggeringObjects.Add(other.gameObject);
        OnObjectTriggered?.Invoke(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        triggeringObjects.Remove(other.gameObject);
    }

    private void FixedUpdate() {
        if (OnObjectTriggering == null) return;

        foreach (GameObject obj in triggeringObjects.ToArray()) {
            OnObjectTriggering.Invoke(obj);
        }
    }
}
