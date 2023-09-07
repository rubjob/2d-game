using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrimaryAttack : BasePlayerState
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    PlayerController playerController;
    MouseUtil mouseUtil;
    private bool isAttacking = false;

    private void Start()
    {
        Setup();
        
        playerController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>();
        mouseUtil = GameObject.FindGameObjectWithTag("MouseUtil").GetComponent<MouseUtil>();
    }

    private void Update()
    {
        // Play animation
        Vector2 velocity = player.GetComponent<Rigidbody2D>().velocity;
        bool isMoving = velocity != Vector2.zero;
        
        animator.SetBool("isMoving", isMoving);
        if (isMoving && velocity.x != 0)
        {
            spriteRenderer.flipX = velocity.x < 0;
        }
    }

    protected override void Action(GameObject[] targets)
    {
        float mAngle = mouseUtil.GetMouseAngle();

        if (mAngle <= 45 && mAngle >= -45)
        {
            //attack right animation
            spriteRenderer.flipX = false;
            animator.SetBool("isAttackingSide", true);
        }
        else if (mAngle >= 135 || mAngle <= -135)
        {
            //attack left animation
            spriteRenderer.flipX = true;
            animator.SetBool("isAttackingSide", true);
        }
        else if (mAngle < -45 && mAngle > -135)
        {
            //attack down animation
            animator.SetBool("isAttackingDown", true);
        }
        else if (mAngle > 45 && mAngle < 135)
        {
            //attack up animation
            animator.SetBool("isAttackingUp", true);
        }

        //trigger normal attack and deal damage to entity in hitbox.
        animator.SetTrigger("Attack");
        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<BaseEntity>().takeDamage(attackDamage);
            }
        }
    }

    public void LockMovement()
    {
        isAttacking = true;
        animator.speed = attackSpeed;
        playerController.movementSpeed = 0;
    }

    public void UnlockMovement()
    {
        isAttacking = false;
        animator.speed = 1f;
        playerController.movementSpeed = 5f;
        
        //reset animation condition
        animator.SetBool("isAttackingSide", false);
        animator.SetBool("isAttackingDown", false);
        animator.SetBool("isAttackingUp", false);
    }

}
