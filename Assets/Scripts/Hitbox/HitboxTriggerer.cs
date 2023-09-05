using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitboxTriggerer : MonoBehaviour
{
    private HashSet<GameObject> triggeringObjects = new HashSet<GameObject>();

    public GameObject[] TriggeringObjects
    {
        get { return triggeringObjects.ToArray<GameObject>(); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BaseEntity>() != null)
        {
            triggeringObjects.Add(other.gameObject);
        }
        // triggeringObject = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        triggeringObjects.Remove(other.gameObject);
        // triggeringObject = null;
    }
}
