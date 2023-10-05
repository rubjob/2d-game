using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.Events;

public class ActionManager : MonoBehaviour {
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

        StartCoroutine(DetectAction());
    }

    // Co-routine for detecting input and executing its action. This runs in parallel in background.
    private IEnumerator DetectAction() {
        while (true) {
            foreach (KeyValuePair<BindingState, EntityStateBinding> e in states) {
                if (e.Value.InputBinding.action.IsInProgress()) {
                    SetState(e.Key);
                    OnPerformingAction?.Invoke();
                    animator.SetTrigger(GetCurrentState().AnimationTriggerName);

                    // Call perform action and wait for function to return
                    yield return StartCoroutine(GetCurrentState().EntityState.PerformAction());
                }
            }

            yield return new WaitForFixedUpdate();
        }
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
}
