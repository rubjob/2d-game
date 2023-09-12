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
    public float attackSpeed = 1f;
    private float attackDelay, lastAttackTime;
    protected float attackWindow;

    protected void Setup()
    {
        attackDelay = 1f / attackSpeed;
        lastAttackTime = -attackDelay;
    }

    protected abstract void Action(GameObject[] targets);
    public void PerformAction()
    {
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