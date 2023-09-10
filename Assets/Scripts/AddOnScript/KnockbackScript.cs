using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour
{
    public Rigidbody2D rb;

    public void Knockback(Vector2 direction, float strength, float delay)
    {
        StopAllCoroutines();
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset(delay));
    }

    private IEnumerator Reset(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
    }
}
