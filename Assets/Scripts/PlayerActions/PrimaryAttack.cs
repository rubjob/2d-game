using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrimaryAttack : BaseEntityState {
    [Header("Dependency")]
    public Rigidbody2D rb;
    public Animator animator;
    public KnockbackScript KnockbackScript;

    [Header("Attack")]
    [SerializeField] private HitboxManager hitbox;
    [SerializeField] private int[] attackDamage = { 80, 120, 250 };
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackWindow = 0;
    [SerializeField] private float attackCooldown = 1.5f;
    private float lastAttackTime = float.MinValue;
    private int comboCount = 1;

    [Header("Events")]
    public UnityEvent<GameObject, Vector2> OnTargetHit;
    public UnityEvent<int> OnTargetsHit;

    public override HitboxManager Hitbox => hitbox;
    public override float AttackDamage => attackDamage[comboCount - 1];
    public override float AttackSpeed => (comboCount == 3) ? attackSpeed / 1.5f : attackSpeed;
    private string AnimationTriggerer => "isAttacking" + comboCount;
    public override float CooldownDuration => attackCooldown;

    private void Start()
    {
        if (attackWindow == 0f)
            attackWindow = 1f / attackSpeed * 2f;
    }

    public override IEnumerator OnPlayingAnimation() {
        SuccessiveAttack();

        animator.speed = AttackSpeed;
        animator.SetTrigger(AnimationTriggerer);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1f / AttackSpeed);
    }

    public override void OnDealingDamage() {
        GameObject[] targets = hitbox.Trigger.TriggeringObjects;

        if (targets.Length > 0) {
            for (int i = 0; i < targets.Length; i++) {
                HealthScript HealthScript;
                if ((HealthScript = targets[i].GetComponent<HealthScript>()) == null) continue;

                HealthScript.TakeDamage(AttackDamage);

                Rigidbody2D targetRb = targets[i].GetComponent<Rigidbody2D>();
                Vector2 direction = (targetRb.position - rb.position).normalized;

                DamagePopup.Create(targetRb.position, AttackDamage, true);

                KnockbackScript.Index = comboCount - 1;
                OnTargetHit?.Invoke(targets[i], direction);
            }

            OnTargetsHit?.Invoke(targets.Length);
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