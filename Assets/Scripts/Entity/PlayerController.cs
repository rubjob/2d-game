using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float MovementSpeed = 5f;

    [Header("Input")]
    public InputActionReference MovementInput;

    [Header("Dependency")]
    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private bool onPerformingAction = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Movement
        if (!onPerformingAction)
        {
            rb.velocity = MovementInput.action.ReadValue<Vector2>().normalized * MovementSpeed;
        }
    }

    private void Update() {
        // Play animation direction
        if (!onPerformingAction) {
            Vector2 velocity = rb.velocity;
            bool isMoving = velocity != Vector2.zero;

            animator.SetBool("isMoving", isMoving);
            if (isMoving && Mathf.Abs(velocity.x) > 0.01f)
                spriteRenderer.flipX = velocity.x < 0;
        }
    }

    public void OnActionStarting() {
        onPerformingAction = true;
    }

    public void OnActionEnded() {
        onPerformingAction = false;
    }
}
