using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseEntity
{
    [Header("Player Controller")]
    public float defaultMovementSpeed = 5f;
    public float movementSpeed = 5f;

    [Header("Mouse Util")]
    public MouseUtil mouseUtil;

    [Header("Dash Script")]
    public DashScript dash;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private void Start()
    {
        Setup();

        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        // Movement
        if (!dash.IsDashing)
        {
            velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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

        if (Input.GetAxis("Fire1") == 1)
            PerformAction(BindingState.PrimaryAttack);
        else if (Input.GetAxis("Fire2") == 1)
            PerformAction(BindingState.HeavyAttack);
        else if (Input.GetAxis("Jump") == 1)
            dash.Dash();
    }

    public void LockMovement()
    {
        float attackSpeed = GetCurrentState().EntityState.attackSpeed;
        animator.speed = (attackSpeed >= 1) ? attackSpeed : 1f;
        movementSpeed = 0;
    }

    public void UnlockMovement()
    {
        animator.speed = 1f;
        movementSpeed = defaultMovementSpeed;
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
