using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackScript : MonoBehaviour
{
    [Header("Dependency")]
    public Rigidbody2D rb;

    [Header("Events")]
    public UnityEvent StartKnockback;
    public UnityEvent EndedKnockback;

    public void Knockback(Vector2 direction, float strength, float delay)
    {
        StopAllCoroutines();
        StartKnockback?.Invoke();
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset(delay));
    }

    private IEnumerator Reset(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.275f);
        EndedKnockback?.Invoke();
    }
}
