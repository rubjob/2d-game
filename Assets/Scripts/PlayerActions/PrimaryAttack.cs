using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryAttack : BaseEntityState {
    [Header("Dependency")]
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Attack")]
    [SerializeField] private HitboxManager hitbox;
    [SerializeField] private int[] attackDamage = { 80, 120, 250 };
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackWindow = 0;
    private float lastAttackTime = float.MinValue;
    private int comboCount = 1;

    [Header("Knockback")]
    public float[] knockbackStrength = { 5f, 5f, 7f };
    public float knockbackDelay = 0.15f;

    public override HitboxManager Hitbox => hitbox;
    public override float AttackDamage => attackDamage[comboCount - 1];
    public override float AttackSpeed => (comboCount == 3) ? attackSpeed / 1.5f : attackSpeed;
    private string AnimationTriggerer => "isAttacking" + comboCount;
    public override float CooldownDuration => 0;

    private void Start()
    {
        if (attackWindow == 0f)
            attackWindow = 1f / attackSpeed * 2f;
    }

    public override IEnumerator OnPlayingAnimation() {
        SuccessiveAttack();

        animator.speed = Mathf.Clamp(AttackSpeed, 1, float.MaxValue);
        animator.SetTrigger(AnimationTriggerer);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1f / AttackSpeed);
    }

    public override void OnDealingDamage() {
        GameObject[] targets = hitbox.Trigger.TriggeringObjects;

        if (targets.Length > 0) {
            for (int i = 0; i < targets.Length; i++) {
                targets[i].GetComponent<HealthScript>().TakeDamage(AttackDamage);

                KnockbackScript kb = targets[i].GetComponent<KnockbackScript>();
                Vector2 direction = (kb.rb.position - rb.position).normalized;
                kb.Knockback(direction, knockbackStrength[comboCount - 1], knockbackDelay);
            }
        }

        lastAttackTime = Time.time;
    }

    private void SuccessiveAttack() {
        if (Time.time - lastAttackTime <= attackWindow && comboCount < 3)
            comboCount += 1;
        else
            comboCount = 1;
    }

    public int getComboCount(int comboCount){
        return comboCount;
    }
}