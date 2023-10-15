using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDash : MonoBehaviour
{
    [Header("Dependecy")]
    public Animator Animator;
    public EntityMover EntityMover;
    public Rigidbody2D Rb;

    [Header("Constant")]
    public float DashDuration;
    public float DashSpeedMultiplier;
    public float DashCooldown;

    [Header("Events")]
    public UnityEvent OnDashStart, OnDashEnd;

    private float LastDash = 0f;

    public void Dash(Vector2 direction) {
        if (direction == Vector2.zero) return;

        if (Time.time >= LastDash + DashCooldown) {
            Debug.Log(LastDash + DashCooldown + ", " + Time.time);
            StopAllCoroutines();
            StartCoroutine(CoDash(direction));
        }
    }

    private IEnumerator CoDash(Vector2 direction) {
        LastDash = Time.time;
        OnDashStart?.Invoke();

        Debug.Log(direction.normalized * DashSpeedMultiplier);

        Animator.SetBool("isMoving", false);

        Rb.velocity = direction.normalized * DashSpeedMultiplier;
        Animator.speed = 0.35f / DashDuration;
        Animator.SetTrigger("Dash");

        yield return new WaitForSeconds(DashDuration);

        Animator.speed = 1f;
        Rb.velocity = Vector2.zero;
        OnDashEnd?.Invoke();
    }
}
