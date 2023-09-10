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

    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Entity States")]
    public BindingState currentState;
    public EntityStateBinding[] stateBindings;
    private Dictionary<BindingState, EntityStateBinding> states = new Dictionary<BindingState, EntityStateBinding>();

    protected float health;

    protected void Setup()
    {
        health = maxHealth;

        foreach (EntityStateBinding e in stateBindings)
            states.Add(e.StateBinding, e);
    }

    // State
    protected void SetState(BindingState state)
    {
        if (GetCurrentState().EntityState.IsReadyToChange())
            currentState = state;
    }

    protected EntityStateBinding GetCurrentState()
    {
        return states[currentState];
    }

    // Combat
    protected void UpdateHitBox(float angle)
    {
        foreach (KeyValuePair<BindingState, EntityStateBinding> entry in states)
        {
            entry.Value.EntityState.hitbox.RotateTo(angle);
        }
    }

    protected void PerformAction(BindingState state)
    {
        SetState(state);

        if (GetCurrentState().EntityState.IsReadyToChange()) {
            OnPerformingAction();
            animator.SetTrigger(GetCurrentState().AnimationTriggerName);
        }

        GetCurrentState().EntityState.PerformAction();
    }

    public void TakeDamage(float amount)
    {
        if (!IsInvulnerable)
            health -= amount;

        if (health <= 0f)
            OnDead();
        else
            OnTakenDamage(amount);
    }

    public void Heal(float amount)
    {
        if (!IsInvulnerable) health += amount;

        if (health > maxHealth) health = maxHealth;
    }

    // Abstract method
    protected abstract void OnPerformingAction();
    protected abstract void OnTakenDamage(float damage);
    protected abstract void OnDead();

    // Getters and Setters
    public float Health
    {
        get { return health; }
    }
}
