using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;

public class BaseEntityState : MonoBehaviour
{
    public HitboxManager hitbox;
    public int attackDamage = 20;
    public float attackSpeed = 1f;

    private float attackDelay, lastAttackTime;

    public UnityEvent<GameObject[]> Action;

    private void Start() {
        attackDelay = 1f / attackSpeed;
        lastAttackTime = -attackDelay;
    }

    public IEnumerator PerformAction() {
        Action?.Invoke(hitbox.Trigger.TriggeringObjects);
        yield return new WaitForSeconds(attackDelay);
    }

    public bool IsReadyToChange()
    {
        return Time.time >= lastAttackTime + attackDelay;
    }
}