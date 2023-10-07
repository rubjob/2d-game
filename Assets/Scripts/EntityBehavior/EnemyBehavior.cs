using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Denpendency")]
    public GameObject TargetObject;
    public Rigidbody2D rb;

    [Header("Range")]
    public float FollowingRange = 5f;
    public float AttackingRange = 3f;

    [Header("Events")]
    public UnityEvent<Vector2> OnMovement;
    public UnityEvent OnAttacking;

    private Rigidbody2D targetRb;

    private void Start() {
        targetRb = TargetObject?.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (!targetRb) return;

        float distance = Vector2.Distance(rb.position, targetRb.position);

        if (distance <= FollowingRange) {
            if (distance <= AttackingRange) {
                OnMovement?.Invoke(Vector2.zero);
                OnAttacking?.Invoke();
            } else {
                Vector2 direction = (targetRb.position - rb.position).normalized;
                OnMovement?.Invoke(direction);
            }
        } else {
            OnMovement?.Invoke(Vector2.zero);
        }
    }
}
