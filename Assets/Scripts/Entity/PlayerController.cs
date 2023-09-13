using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseEntity
{
    [Header("Player Controller")]
    public float defaultMovementSpeed = 5f;
    public float movementSpeed = 5f;

    [Header("Input")]
    public InputActionReference MovementInput;
    public InputActionReference NormalAttack;
    public InputActionReference HeavyAttack;
    public InputActionReference Dash;

    [Header("Mouse Util")]
    public MouseUtil mouseUtil;

    [Header("Dash Script")]
    public DashScript dash;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private bool inAction;

    private void Start()
    {
        Setup();

        rb = GetComponent<Rigidbody2D>();
        inAction = false;
    }

    private void FixedUpdate()
    {
        // Movement
        if (!dash.IsDashing)
        {
            velocity = MovementInput.action.ReadValue<Vector2>();
            velocity = velocity.normalized * movementSpeed;
            rb.velocity = velocity;
            velocity = GetComponent<Rigidbody2D>().velocity;
            // Play walking animation
            bool isMoving = velocity != Vector2.zero;
            animator.SetBool("isMoving", isMoving);
            if (isMoving && velocity.x != 0)
            {
                spriteRenderer.flipX = velocity.x < 0;
            }
        }


        // Combat input
        UpdateHitBox(mouseUtil.GetMouseAngle());

        if (NormalAttack.action.IsInProgress())
            PerformAction(BindingState.PrimaryAttack);
        else if (HeavyAttack.action.IsInProgress())
            PerformAction(BindingState.HeavyAttack);
        else if (Dash.action.IsInProgress())
            dash.Dash();

    }
    public void LockMovement()
    {
        float attackSpeed = GetCurrentState().EntityState.attackSpeed;
        animator.speed = (attackSpeed >= 1) ? attackSpeed : 1f;
        movementSpeed = 0;
        inAction = true;
    }

    public void UnlockMovement()
    {
        animator.speed = 1f;
        movementSpeed = defaultMovementSpeed;
        inAction = false;
    }

    protected override void OnPerformingAction()
    {
        float mAngle = mouseUtil.GetMouseAngle();
        if (mAngle <= 45 && mAngle >= -45)
        {
            //attack right animation
            spriteRenderer.flipX = false;
            animator.SetTrigger("isAttackingSide");
        }
        else if (mAngle >= 135 || mAngle <= -135)
        {
            //attack left animation
            spriteRenderer.flipX = true;
            animator.SetTrigger("isAttackingSide");
        }
        else if (mAngle < -45 && mAngle > -135)
        {
            //attack down animation
            animator.SetTrigger("isAttackingDown");
        }
        else if (mAngle > 45 && mAngle < 135)
        {
            //attack up animation
            animator.SetTrigger("isAttackingUp");
        }
    }
}
