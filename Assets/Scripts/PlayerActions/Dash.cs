using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : BaseEntityState
{
    [Header("Dependency")]
    public EntityMover EntityMover;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    [Header("Dash")]
    public float DashSpeedMultipiler = 5f;
    public float DashDuration = 0.7f;
    [SerializeField] private float DashCooldown = 3f;

    public override IEnumerator OnPlayingAnimation() {
        animator.SetBool("isMoving", false);
        if (rb.velocity == Vector2.zero) {
            rb.velocity = new Vector2(spriteRenderer.flipX ? -EntityMover.MovementSpeed : EntityMover.MovementSpeed, 0);
        }

        rb.velocity *= DashSpeedMultipiler;
        animator.speed = 0.35f / DashDuration;
        animator.SetTrigger("Dash");

        yield return new WaitForSeconds(DashDuration);
    }

    public override void OnDealingDamage() {}

    public override float AttackDamage => throw new System.NotImplementedException();
    public override float AttackSpeed => throw new System.NotImplementedException();
    public override HitboxManager Hitbox => null;
    public override float CooldownDuration => DashCooldown;
}
