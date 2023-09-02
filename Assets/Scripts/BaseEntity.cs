using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {
    [Header("Base Entity")]
    public bool IsInvulnerable = false;
    public float maxHealth = 100f;

    protected float health;

    private void Start() {
        health = maxHealth;
    }

    public float Health {
        get { return health; }
    }

    public void takeDamage(float amount) {
        if (!IsInvulnerable) {
            health -= amount;
        }

        if (health <= 0f) {
            OnDead();
        } else {
            OnTakenDamage(amount);
        }
    }

    public void heal(float amount) {
        if (!IsInvulnerable) {
            health += amount;
        }

        if (health > maxHealth) health = maxHealth;
    }

    protected abstract void OnTakenDamage(float damage);
    protected abstract void OnDead();
}
