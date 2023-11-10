using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    [SerializeField] private HitboxTriggerer Trigger;
    private BoxCollider2D boxCollider;
    private Vector2 originalOffset;

    private void Start() {
        boxCollider = Trigger.GetComponent<BoxCollider2D>();
        originalOffset = boxCollider.offset;
    }

    public GameObject[] GetCollidedObjects() {
        List<GameObject> objs = new();
        
        foreach (GameObject obj in Trigger.TriggeringObjects)
            if (obj.GetComponent<HealthScript>())
                objs.Add(obj);

        return objs.ToArray();
    }

    public void RotateTo(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void FlipX(bool value) {
        boxCollider.offset = originalOffset * new Vector2(value ? -1 : 1, 1);
    }
}
