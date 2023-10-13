using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour
{
    public bool isInvulnerable = false;
    public float maxHealth = 100f;
    public HealthBar healthBar;

    public UnityEvent<float> OnHeal, OnTakenDamage;
    public UnityEvent OnDead;

    private float health;


    void Start()
    {
        health = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    /*
     * Method
     */
    public void TakeDamage(float amount) => AdjustHealth(-amount);
    public void Heal(float amount) => AdjustHealth(amount);
    private void AdjustHealth(float amount)
    {
        if (!isInvulnerable)
            health += amount;

        health = Mathf.Clamp(health, 0, maxHealth);

        healthBar.setHealth(health);

        if (health <= 0f) OnDead?.Invoke();
        if (health <= 0f) {
            Destroy(gameObject);
            OnDead?.Invoke();
        }
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
