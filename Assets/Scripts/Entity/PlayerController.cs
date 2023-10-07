using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Dependency")]
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public EntityMover EntityMover;

    [Header("Input")]
    public InputActionReference MovementInput;

    private void Update() {
        if (EntityMover.IsBlockingMovement) return;

        Vector2 velocity = rb.velocity;
        bool isMoving = velocity != Vector2.zero;

        animator.SetBool("isMoving", isMoving);
        if (isMoving && Mathf.Abs(velocity.x) > 0.01f)
            spriteRenderer.flipX = velocity.x < 0;
    }

    private void FixedUpdate() {
        EntityMover.MoveToDirection(MovementInput.action.ReadValue<Vector2>());
    }

    public void OnActionStarting() {
        EntityMover.IsBlockingMovement = true;
    }

    public void OnActionEnded() {
        EntityMover.IsBlockingMovement = false;
    }
}
