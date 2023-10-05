using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BaseEntityState))]
public class PrimaryAttack : MonoBehaviour
{
    public PlayerController playerController;
    public float knockbackStrength = 5f;
    public float knockbackDelay = 0.15f;
    public float attackWindow;

    private float lastAttackTime = float.MinValue;

    private int comboCount;
    private Rigidbody2D rb;

    private BaseEntityState baseEntityState;

    private void Start()
    {
        baseEntityState = GetComponent<BaseEntityState>();
        rb = playerController.GetComponent<Rigidbody2D>();

        attackWindow = 1f / baseEntityState.attackSpeed * 1.5f;
        comboCount = 0;
    }

    public void Action(GameObject[] targets)
    {
        switch (SuccessiveAttack())
        {
            case 1:
                baseEntityState.attackDamage = 80;
                playerController.animator.SetTrigger("isAttacking1");
                break;
            case 2:
                baseEntityState.attackDamage = 120;
                playerController.animator.SetTrigger("isAttacking2");
                break;
            case 3:
                baseEntityState.attackDamage = 250;
                playerController.animator.SetTrigger("isAttacking3");
                break;
            default:
                break;
        }

        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<HealthScript>().TakeDamage(baseEntityState.attackDamage);

                KnockbackScript kb = targets[i].GetComponent<KnockbackScript>();
                Vector2 direction = (kb.rb.position - rb.position).normalized;
                kb.Knockback(direction, knockbackStrength, knockbackDelay);
            }
        }

        lastAttackTime = Time.time;
    }
    private int SuccessiveAttack()
    {
        if (Time.time - lastAttackTime <= attackWindow && comboCount < 3)
        {
            comboCount += 1;
        }
        else
        {
            comboCount = 1;
        }
        return comboCount;
    }
}