using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    [Header("Dependency")]
    public EntityMover EntityMover;
    public EnemyBehavior EnemyBehavior;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public Rigidbody2D rb;

    [Header("Attack")]
    public float AttackDamage = 50f;

    private void Update()
    {
        if (EntityMover.IsBlockingMovement) return;

        Vector2 velocity = rb.velocity;
        bool isMoving = velocity != Vector2.zero;

        Animator.SetBool("isMoving", isMoving);
        if (isMoving && Mathf.Abs(velocity.x) > 0.01f)
            SpriteRenderer.flipX = velocity.x < 0;
    }

    public void OnAnimation()
    {
        Animator.SetTrigger("Attack");
    }

    public void OnAttack(GameObject TargetObject)
    {
        HealthScript health = TargetObject.GetComponent<HealthScript>();

        if (!health) return;
        health.TakeDamage(AttackDamage);
    }

    public void LockMovement()
    {
        EntityMover.IsBlockingMovement = true;
        Animator.speed = 1f / EnemyBehavior.AttackDuration;
    }

    public void UnlockMovement()
    {
        EntityMover.IsBlockingMovement = false;
        Animator.speed = 1f;
    }
}
