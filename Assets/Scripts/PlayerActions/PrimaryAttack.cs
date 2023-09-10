using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrimaryAttack : BaseEntityState
{
    private float mAngle;
    private int comboCount;
    public PlayerController playerController;

    private void Start()
    {
        Setup();
        comboCount = 0;
    }

    private void Update() { }

    protected override void Action(GameObject[] targets)
    {
        switch (SuccessiveAttack())
        {
            case 1:
                attackDamage = 80;
                playerController.animator.SetTrigger("isAttacking1");
                break;
            case 2:
                attackDamage = 120;
                playerController.animator.SetTrigger("isAttacking2");
                break;
            case 3:
                attackDamage = 250;
                playerController.animator.SetTrigger("isAttacking3");
                break;
            default:
                break;
        }

        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<BaseEntity>().TakeDamage(attackDamage);
            }
        }
    }
    private int SuccessiveAttack()
    {
        if (attackWindow < 1f && comboCount < 3)
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