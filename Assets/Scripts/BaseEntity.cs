using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {
    [Header("Base Entity")]
    public bool IsInvulnerable = false;
    public float maxHealth = 100f;

    protected float health;

    public BaseEntity() {
        health = maxHealth;
        Debug.Log("Base; Base : "+health + ", " + maxHealth);
    }

    public float Health {
        get { return health; }
    }

    public void takeDamage(float amount) {
        if (!IsInvulnerable) {
            health -= amount;
        }

        Debug.Log("Base; health left from take damage : "+health);
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

        OnHeal(amount);
    }

    protected abstract void OnTakenDamage(float damage);

    protected abstract void OnHeal(float amount);
    protected abstract void OnDead();
}
