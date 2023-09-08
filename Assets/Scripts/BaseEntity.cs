using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
    [Header("Base Entity")]
    public bool IsInvulnerable = false;
    public float maxHealth = 100f;

    [Header("Entity States")]
    public BindingState currentState;
    public EntityStateBinding[] stateBindings;
    private Dictionary<BindingState, BaseEntityState> states = new Dictionary<BindingState, BaseEntityState>();

    protected float health;

    protected void Setup()
    {
        health = maxHealth;

        foreach (EntityStateBinding e in stateBindings)
            states.Add(e.StateBinding, e.EntityState);
    }

    // State
    protected void SetState(BindingState state)
    {
        if (GetCurrentState().IsReadyToChange())
            currentState = state;
    }

    protected BaseEntityState GetCurrentState()
    {
        foreach (KeyValuePair<BindingState, BaseEntityState> entry in states)
            entry.Value.spriteRenderer.enabled = currentState == entry.Key;

        return states[currentState];
    }

    // Combat
    protected void UpdateHitBox(float angle)
    {
        foreach (KeyValuePair<BindingState, BaseEntityState> entry in states)
        {
            entry.Value.hitbox.RotateTo(angle);
        }
    }

    protected void Attack(BindingState state)
    {
        SetState(state);
        GetCurrentState().PerformAction();
    }

    public void TakeDamage(float amount)
    {
        if (!IsInvulnerable)
        {
            health -= amount;
        }

        if (health <= 0f)
        {
            OnDead();
        }
        else
        {
            OnTakenDamage(amount);
        }
    }

    public void Heal(float amount)
    {
        if (!IsInvulnerable)
        {
            health += amount;
        }

        if (health > maxHealth) health = maxHealth;
    }

    // Abstract method
    protected abstract void OnTakenDamage(float damage);
    protected abstract void OnDead();

    // Getters and Setters
    public float Health
    {
        get { return health; }
    }
}
