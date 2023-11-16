using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour
{
    public bool isInvulnerable = false;
    public float MaxHealth { get => BaseMaxHealth + AdditionalMaxHealth; }
    public float BaseMaxHealth = 100f;
    private float AdditionalMaxHealth = 0f;

    public UnityEvent<float> OnHeal, OnTakenDamage, OnHealthChange;
    public UnityEvent OnDead;

    public float Health { get; private set; }

    void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(float amount) => AdjustHealth(-amount);
    public void Heal(float amount) => AdjustHealth(amount);
    private void AdjustHealth(float amount)
    {
        if (!isInvulnerable)
            Health += amount;

        Health = Mathf.Clamp(Health, 0, MaxHealth);
        OnHealthChange?.Invoke(Health);

        if (Health <= 0f) {
            Destroy(gameObject);
            OnDead?.Invoke();
        }
        else if (amount > 0) OnHeal?.Invoke(amount);
        else if (amount < 0) OnTakenDamage?.Invoke(-amount);
    }

    public void SetAdditionalMaxHealth(float Amount) {
        // Assume that max health buff CANNOT be stacked
        AdditionalMaxHealth = Amount;
        Health = Mathf.Clamp(0, Health, MaxHealth);
    }
}
