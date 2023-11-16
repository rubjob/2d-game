using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEnemyController : MonoBehaviour
{
    [Header("Dependency")]
    public EntityMover EntityMover;
    public EnemyBehavior EnemyBehavior;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public Rigidbody2D rb;

    [Header("Smoke")]
    public GameObject SmokeObject;
    private bool hasAttacked = false;
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
        if (!hasAttacked)
        {
            GameObject smoke = Instantiate(SmokeObject);
            Rigidbody2D smokeRb = smoke.AddComponent<Rigidbody2D>();
            smokeRb.gravityScale = 0f;
            smokeRb.position = rb.position;
            hasAttacked = true;
        }
    }

    public void LockMovement()
    {
        EntityMover.IsBlockingMovement = true;
        Animator.speed = 1f / EnemyBehavior.AttackDuration;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void UnlockMovement()
    {
        EntityMover.IsBlockingMovement = false;

        Animator.speed = 1f;
    }
}
