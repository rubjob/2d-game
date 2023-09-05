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

    protected override void Action(GameObject[] targets)
    {
        float mAngle = mouseUtil.GetMouseAngle();
        //attack right animation
        if (mAngle <= 45 && mAngle >= -45)
        {
            anim.SetBool("isAttackingSide", true);
            if (spriteRenderer.flipX)
            {

                spriteRenderer.flipX = false;
            }
        }
        //attack left animation
        if (mAngle >= 135 || mAngle <= -135)
        {
            anim.SetBool("isAttackingSide", true);
            if (!spriteRenderer.flipX)
            {

                spriteRenderer.flipX = true;
            }
        }
        //attack down animation
        if (mAngle < -45 && mAngle > -135)
        {
            anim.SetBool("isAttackingDown", true);
        }
        //attack up animation
        if (mAngle > 45 && mAngle < 135)
        {
            anim.SetBool("isAttackingUp", true);
        }

        //trigger normal attack and deal damage to entity in hitbox.
        if (combo == 0)
        {
            anim.SetTrigger("Attack");
            if (targets.Length > 0)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    // Debug.Log(targets[i].name);
                    targets[i].GetComponent<BaseEntity>().takeDamage(attackDamage);
                }
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
