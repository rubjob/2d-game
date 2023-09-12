using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {
    [Header("Base Entity")]
    public bool IsInvulnerable = false;
    public float maxHealth = 100f;

    protected float health;
    //dev---------------------------------------------------
    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Entity States")]
    public BindingState currentState;
    public EntityStateBinding[] stateBindings;
    private Dictionary<BindingState, EntityStateBinding> states = new Dictionary<BindingState, EntityStateBinding>();

    protected void Setup()
    {
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

    protected abstract void OnPerformingAction();


    //----------------------------------------------------------------------------

    // public BaseEntity() {
    //     health = maxHealth;
    //     Debug.Log("Base; Base : "+health + ", " + maxHealth);
    // }

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

    


    protected abstract void OnTakenDamage(float damage);
    protected abstract void OnHeal(float amount);
    protected abstract void OnDead();



}
