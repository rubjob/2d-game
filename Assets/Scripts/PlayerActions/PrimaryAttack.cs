using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryAttack : BasePlayerState {
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private void Update() {
        // Play animation
        Vector2 velocity = player.GetComponent<Rigidbody2D>().velocity;
        bool isMoving = velocity != Vector2.zero;

        animator.SetBool("isMoving", isMoving);
        if (isMoving && velocity.x != 0) {
            spriteRenderer.flipX = velocity.x < 0;
        }
    }

    protected override void Action(GameObject target) {
        if (target != null) {
            target.GetComponent<BaseEntity>().takeDamage(attackDamage);
        }
    }
}
