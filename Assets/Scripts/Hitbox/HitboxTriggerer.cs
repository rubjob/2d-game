using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxTriggerer : MonoBehaviour
{
    private GameObject triggeringObject = null;
    public GameObject TriggeringObject {
        get { return triggeringObject; }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        triggeringObject = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other) {
        triggeringObject = null;
    }
}
