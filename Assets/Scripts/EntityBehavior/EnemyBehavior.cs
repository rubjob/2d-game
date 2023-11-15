using System;
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
    public float AttackDelay = 1f;
    public float AttackDuration = 1f;
    public float AttackCooldown = 1f;
    public bool ByPassAttackRange = false;

    private float LastAttackTime = 0f;

    [Header("Events")]
    public UnityEvent<Vector2> OnMovement;
    public UnityEvent OnAnimation;
    public UnityEvent<GameObject> OnAttacking;

    private Rigidbody2D targetRb;

    private void Start()
    {
        if (!TargetObject)
            TargetObject = GameObject.FindGameObjectWithTag("Player");

        targetRb = TargetObject?.GetComponent<Rigidbody2D>();

        StartCoroutine(PerformBehavior());
    }

    private IEnumerator PerformBehavior()
    {
        LastAttackTime = Time.time;

        while (true)
        {
            if (!TargetObject || !targetRb)
            {
                OnMovement?.Invoke(Vector2.zero);
                yield return new WaitForFixedUpdate();
                continue;
            }

            float distance = Vector2.Distance(rb.position, targetRb.position);

            if (distance <= FollowingRange)
            {
                if (distance <= AttackingRange)
                {
                    OnMovement?.Invoke(Vector2.zero);

                    if (Time.time >= LastAttackTime + AttackCooldown)
                    {
                        yield return new WaitForSeconds(AttackDelay);

                        OnAnimation?.Invoke();

                        yield return new WaitForSeconds(AttackDuration);

                        LastAttackTime = Time.time;
                    }
                }
                else
                {
                    Vector2 direction = (targetRb.position - rb.position).normalized;
                    OnMovement?.Invoke(direction);
                }
            }
            else
            {
                OnMovement?.Invoke(Vector2.zero);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void SignalAttack()
    {
        if (ByPassAttackRange || Vector2.Distance(rb.position, targetRb.position) <= AttackingRange)
            OnAttacking?.Invoke(TargetObject);
    }

    public void Interrupt(float duration)
    {
        StopCoroutine("CoInterrupt");
        StartCoroutine(CoInterrupt(duration));
    }

    private IEnumerator CoInterrupt(float duration)
    {
        OnMovement?.Invoke(Vector2.zero);
        StopCoroutine("PerformBehavior");

        yield return new WaitForSeconds(duration);

        StartCoroutine(PerformBehavior());
    }
}
