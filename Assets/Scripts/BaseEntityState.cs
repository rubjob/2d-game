using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public abstract class BaseEntityState : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    public HitboxManager hitbox;
    public int attackDamage = 20;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float attackSpeed = 1f;
    private float attackDelay, lastAttackTime;
    protected float attackWindow;
    private MouseUtil mouseUtil;

    protected void Setup()
    {
        attackDelay = 1f / attackSpeed;
        lastAttackTime = -attackDelay;
        mouseUtil = GameObject.FindGameObjectWithTag("MouseUtil").GetComponent<MouseUtil>();

    }

    protected abstract void Action(GameObject[] targets);
    public void PerformAction()
    {
        float mAngle = mouseUtil.GetMouseAngle();

        if (mAngle <= 45 && mAngle >= -45)
        {
            spriteRenderer.flipX = false;
        }
        else if (mAngle >= 135 || mAngle <= -135)
        {
            spriteRenderer.flipX = true;
        }

        float currentTime = Time.time;
        if (currentTime >= lastAttackTime + attackDelay)
        {
            attackWindow = currentTime - lastAttackTime;
            lastAttackTime = currentTime;
            Action(hitbox.Trigger.TriggeringObjects);
        }
    }

    public bool IsReadyToChange()
    {
        return Time.time >= lastAttackTime + attackDelay;
    }
}