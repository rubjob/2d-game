using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryAttack : BasePlayerState
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    PlayerController playerController;
    private Animator anim;

    int combo;



    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>();
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

        if (combo == 0)
        {
            anim.SetTrigger("Attack");

        }
        if (target != null)
        {
            target.GetComponent<BaseEntity>().takeDamage(attackDamage);

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

    }

}
