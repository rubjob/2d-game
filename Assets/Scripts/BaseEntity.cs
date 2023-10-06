using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {
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





}

