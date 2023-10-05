using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.Events;

public class BaseEntity : MonoBehaviour {
    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Entity States")]
    public BindingState currentState;
    public EntityStateBinding[] stateBindings;
    private Dictionary<BindingState, EntityStateBinding> states = new Dictionary<BindingState, EntityStateBinding>();

    [Header("Callback")]
    public UnityEvent OnPerformingAction;

    private void Start() {
        foreach (EntityStateBinding e in stateBindings)
            states.Add(e.StateBinding, e);
    }

    // State
    public void SetState(BindingState state) {
        if (GetCurrentState().EntityState.IsReadyToChange())
            currentState = state;
    }

    public EntityStateBinding GetCurrentState() {
        return states[currentState];
    }

    // Combat
    public void UpdateHitBox(float angle) {
        foreach (KeyValuePair<BindingState, EntityStateBinding> entry in states) {
            entry.Value.EntityState.hitbox.RotateTo(angle);
        }
    }

    public void PerformAction(BindingState state) {
        SetState(state);

        if (GetCurrentState().EntityState.IsReadyToChange()) {
            OnPerformingAction?.Invoke();
            animator.SetTrigger(GetCurrentState().AnimationTriggerName);
        }

        GetCurrentState().EntityState.PerformAction();
    }
}
