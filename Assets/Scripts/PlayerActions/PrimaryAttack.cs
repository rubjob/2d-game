using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryAttack : BasePlayerState
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    PlayerController playerController;
    MouseUtil mouseUtil;
    private Animator anim;

    int combo;



    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>();
        mouseUtil = GameObject.FindGameObjectWithTag("MouseUtil").GetComponent<MouseUtil>();
        anim = GetComponent<Animator>();
        combo = 0;
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

    protected override void Action(GameObject target)
    {
        float mAngle = mouseUtil.GetMouseAngle();
        Debug.Log(mAngle);
        //attack right animation
        if (mAngle <= 30 && mAngle >= -30)
        {
            anim.SetBool("isAttackingSide", true);
            if (spriteRenderer.flipX)
            {

                spriteRenderer.flipX = false;
            }
        }
        //attack left animation
        if (mAngle >= 150 || mAngle <= -150)
        {
            anim.SetBool("isAttackingSide", true);
            if (!spriteRenderer.flipX)
            {

                spriteRenderer.flipX = true;
            }
        }
        //attack down animation
        if (mAngle < -30 && mAngle > -150)
        {
            anim.SetBool("isAttackingDown", true);
        }
        //attack up animation
        if (mAngle > 30 && mAngle < 150)
        {
            anim.SetBool("isAttackingUp", true);
        }

        //trigger normal attack and deal damage to entity in hitbox.
        if (combo == 0)
        {
            anim.SetTrigger("Attack");
            if (target != null)
            {
                target.GetComponent<BaseEntity>().takeDamage(attackDamage);

            }

        }
    }
    public void lockMovement()
    {
        playerController.movementSpeed = 0;
        combo += 1;
    }
    public void unlockMovement()
    {
        playerController.movementSpeed = 5f;
        combo -= 1;
        //reset animation condition
        anim.SetBool("isAttackingSide", false);
        anim.SetBool("isAttackingDown", false);
        anim.SetBool("isAttackingUp", false);
    }

}
