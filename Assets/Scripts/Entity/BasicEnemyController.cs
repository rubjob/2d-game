using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    [Header("Dependency")]
    public EntityMover EntityMover;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public Rigidbody2D rb;

    private void Update() {
        if (EntityMover.IsBlockingMovement) return;

        Vector2 velocity = rb.velocity;
        bool isMoving = velocity != Vector2.zero;

        Animator.SetBool("isMoving", isMoving);
        if (isMoving && Mathf.Abs(velocity.x) > 0.01f)
            SpriteRenderer.flipX = velocity.x < 0;
    }
}
