using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Denpendency")]
    public GameObject TargetObject;
    public Rigidbody2D rb;

    [Header("Constant")]
    public float FollowingRange = 5f;
    public float AttackingRange = 3f;
    public float AttackCooldown = 1f;
    public float AttackDuration = 1f;

    private float LastAttackTime = 0f;

    [Header("Events")]
    public UnityEvent<Vector2> OnMovement;
    public UnityEvent OnAttacking;

    private Rigidbody2D targetRb;

    private void Start() {
        targetRb = TargetObject?.GetComponent<Rigidbody2D>();

        StartCoroutine(PerformBehavior());
    }

    private IEnumerator PerformBehavior() {
        while (TargetObject) {
            if (!targetRb) continue;

            float distance = Vector2.Distance(rb.position, targetRb.position);

            if (distance <= FollowingRange) {
                if (distance <= AttackingRange) {
                    OnMovement?.Invoke(Vector2.zero);

                    if (Time.time >= LastAttackTime + AttackCooldown) {
                        OnAttacking?.Invoke();
                        yield return new WaitForSeconds(AttackDuration);

                        LastAttackTime = Time.time;
                    }
                } else {
                    Vector2 direction = (targetRb.position - rb.position).normalized;
                    OnMovement?.Invoke(direction);
                }
            } else {
                OnMovement?.Invoke(Vector2.zero);
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
