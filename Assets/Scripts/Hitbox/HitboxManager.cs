using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    public HitboxTriggerer Trigger;

    private BoxCollider2D boxCollider;
    private Vector2 originalOffset;

    private void Start() {
        boxCollider = Trigger.GetComponent<BoxCollider2D>();
        originalOffset = boxCollider.offset;
    }

    public void RotateTo(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void FlipX(bool value) {
        boxCollider.offset = originalOffset * new Vector2(value ? -1 : 1, 1);
    }
}
