using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerState : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    public HitboxManager hitbox;
    public int attackDamage = 20;
    public float attackSpeed = 1f;

    private float attackDelay, lastAttackTime;

    public BasePlayerState()
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
            lastAttackTime = currentTime;

            Action(hitbox.Trigger.TriggeringObjects);
        }
    }
}