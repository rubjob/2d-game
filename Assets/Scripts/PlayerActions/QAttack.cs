using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QAttack : BaseEntityState
{
    [Header("Dependency")]
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Attack")]
    [SerializeField] private HitboxManager hitbox;
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float attackSpeed = 1.5f;
    [SerializeField] private float cooldownTime = 1.5f;

    [Header("Knockback")]
    public float knockbackStrength = 5f;
    public float knockbackDelay = 0.15f;

    public override float AttackDamage => attackDamage;
    public override float AttackSpeed => attackSpeed;
    public override HitboxManager Hitbox => hitbox;
    public override float CooldownDuration => cooldownTime;

    public override IEnumerator OnPlayingAnimation()
    {
        animator.speed = AttackSpeed;
        animator.SetTrigger("QAttack");

        yield return new WaitForSeconds(1f / AttackSpeed);
    }

    public override void OnDealingDamage()
    {
        GameObject[] targets = hitbox.Trigger.TriggeringObjects;

        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<HealthScript>().TakeDamage(AttackDamage);

                KnockbackScript kb = targets[i].GetComponent<KnockbackScript>();
                Vector2 direction = (kb.rb.position - rb.position).normalized;
                kb.Knockback(direction, knockbackStrength, knockbackDelay);

                DamagePopup.Create(kb.rb.position,AttackDamage);
            }
        }
   
    }
}
