using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller")]
    public float defaultMovementSpeed = 5f;
    public float movementSpeed = 5f;

    [Header("Input")]
    public InputActionReference MovementInput;
    public InputActionReference Dash;

    [Header("Dash Script")]
    public DashScript dash;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;

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
            velocity = MovementInput.action.ReadValue<Vector2>();
            velocity = velocity.normalized * movementSpeed;
            rb.velocity = velocity;
            velocity = GetComponent<Rigidbody2D>().velocity;

            // Play animation direction
            bool isMoving = velocity != Vector2.zero;

            animator.SetBool("isMoving", isMoving);
            if (isMoving && velocity.x != 0)
                spriteRenderer.flipX = velocity.x < 0;
        }

        if (Dash.action.IsInProgress() && !onPerformingAction) {
            dash.Dash();
        }
    }

    public void OnActionStarting() {
        onPerformingAction = true;

        if (!dash.IsDashing)
            rb.velocity = Vector2.zero;
    }

    public void OnActionEnded() {
        onPerformingAction = false;
    }
}
