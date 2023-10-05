using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : BaseEntityState
{
    [Header("Dependency")]
    public PlayerController pc;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    [Header("Dash")]
    public float DashSpeedMultipiler = 5f;
    public float DashDuration = 0.7f;
    public float DashCooldown = 0.5f;

    public override IEnumerator OnPlayingAnimation() {
        animator.SetBool("isMoving", false);
        rb.velocity = new Vector2(spriteRenderer.flipX ? -pc.MovementSpeed : pc.MovementSpeed, 0);

        rb.velocity *= DashSpeedMultipiler;
        animator.speed = 0.35f / DashDuration;
        animator.SetTrigger("Dash");

        yield return new WaitForSeconds(DashDuration);
    }

    public override void OnDealingDamage() {}

    public override float AttackDamage => throw new System.NotImplementedException();
    public override float AttackSpeed => throw new System.NotImplementedException();
    public override HitboxManager Hitbox => null;
}
