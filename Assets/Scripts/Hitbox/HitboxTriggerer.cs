using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitboxTriggerer : MonoBehaviour
{
    private readonly HashSet<GameObject> triggeringObjects = new();

    public GameObject[] TriggeringObjects
    {
        get { return triggeringObjects.ToArray<GameObject>(); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggeringObjects.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        triggeringObjects.Remove(other.gameObject);
    }
}
