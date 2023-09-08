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
    
    protected void Setup()
    {
        // attackDelay = 1f / attackSpeed;
        lastAttackTime = -attackDelay;
    }
    private void Start()
    {
        attackDelay = 1f / attackSpeed;
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