using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBody : MonoBehaviour
{
    public Rigidbody2D rb;

    private void OnCollisionExit2D(Collision2D collision) {
        rb.velocity = Vector2.zero;
    }
}
