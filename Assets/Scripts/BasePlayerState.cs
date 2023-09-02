using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerState : MonoBehaviour {
    [SerializeField] protected GameObject player;
    public HitboxManager hitbox;
    public int attackDamage = 20;
    public float attackSpeed = 0.5f;

    private float attackDelay, lastAttackTime;

    private void Start() {
        attackDelay = 1f / attackSpeed;
        lastAttackTime = -attackDelay;
    }

    protected abstract void Action(GameObject target);
    public void PerformAction() {
        float currentTime = Time.time;
        if (currentTime >= lastAttackTime + attackDelay) {
            lastAttackTime = currentTime;

            Action(hitbox.Trigger.TriggeringObject);
        }
    }
}