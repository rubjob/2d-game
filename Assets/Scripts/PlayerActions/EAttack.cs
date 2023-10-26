using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EAttack : BaseEntityState
{
    [Header("Dependency")]
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Attack")]
    [SerializeField] private HitboxManager hitbox;
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float attackSpeed = 1.5f;
    [SerializeField] private float cooldownTime = 1.5f;

    [Header("Events")]
    public UnityEvent<GameObject, Vector2> OnTargetHit;

    public override float AttackDamage => attackDamage;
    public override float AttackSpeed => attackSpeed;
    public override HitboxManager Hitbox => hitbox;
    public override float CooldownDuration => cooldownTime;

    public override IEnumerator OnPlayingAnimation() {
        animator.speed = AttackSpeed;
        animator.SetTrigger("EAttack");

        yield return new WaitForSeconds(1f / AttackSpeed);
    }

    public override void OnDealingDamage() {
        GameObject[] targets = hitbox.Trigger.TriggeringObjects;

        Debug.Log(targets.Length);

        if (targets.Length > 0) {
            for (int i = 0; i < targets.Length; i++) {
                targets[i].GetComponent<HealthScript>().TakeDamage(AttackDamage);

                Rigidbody2D targetRb = targets[i].GetComponent<Rigidbody2D>();
                Vector2 direction = (targetRb.position - rb.position).normalized;

                DamagePopup.Create(targetRb.position, AttackDamage);

                OnTargetHit?.Invoke(targets[i], direction);
            }
        }
    }
}
