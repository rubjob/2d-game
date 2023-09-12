using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour
{
    public bool isInvulnerable = false;
    public float maxHealth = 100f;
    public UnityEvent<float> OnHeal, OnTakenDamage;
    public UnityEvent OnDead;

    private float health;

    void Start()
    {
        health = maxHealth;
    }

    /*
     * Method
     */
    public void TakeDamage(float amount)
    {
        /*if (!isInvulnerable)
            health -= amount;

        if (health <= 0f)
            OnDead?.Invoke();
        else
            OnTakenDamage?.Invoke(amount);*/
        AdjustHealth(-amount);
    }

    public void Heal(float amount)
    {
        /*if (!isInvulnerable)
            health += amount;

        OnHeal?.Invoke(amount);

        if (health > maxHealth) health = maxHealth;*/
        AdjustHealth(amount);
    }

    private void AdjustHealth(float amount)
    {
        if (!isInvulnerable)
            health += amount;

        health = Mathf.Clamp(health, 0, maxHealth);

        if (health <= 0f) OnDead?.Invoke();
        else if (amount > 0) OnHeal?.Invoke(amount);
        else if (amount < 0) OnTakenDamage?.Invoke(-amount);
    }

    /*
     * Getter and Setter
     */
    public float Health {
        get => health;
    }
}