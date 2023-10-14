using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionManager : MonoBehaviour {
    [Header("Animation")]
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Entity States")]
    public StateBinding currentState;
    public EntityStateBinding[] stateBindings;

    private Dictionary<StateBinding, EntityStateBinding> states = new();
    private Dictionary<StateBinding, float> cooldowns = new();

    [Header("Events")]
    public UnityEvent<StateBinding> OnActionStarting;
    public UnityEvent OnActionEnded;

    private void Start() {
        foreach (EntityStateBinding e in stateBindings)
            states.Add(e.StateBinding, e);

        foreach (EntityStateBinding e in stateBindings)
            cooldowns.Add(e.StateBinding, 0);

        StartCoroutine(DetectAction());
    }

    // Co-routine for detecting input and executing its action. This runs in parallel in background.
    private IEnumerator DetectAction() {
        while (true) {
            foreach (KeyValuePair<StateBinding, EntityStateBinding> e in states) {
                StateBinding state = e.Key;
                EntityStateBinding entityBinding = e.Value;

                if (entityBinding.EntityState.StateSignal && cooldowns[state] <= Time.time) {
                    SetState(state);

                    OnActionStarting?.Invoke(state);

                    // Stop movement on starting action
                    if (GetCurrentState().StopOnActionStarting)
                        rb.velocity = Vector2.zero;

                    // Play animation and wait for return
                    yield return StartCoroutine(entityBinding.EntityState.OnPlayingAnimation());

                    // Set cooldown
                    cooldowns[state] = Time.time + GetCurrentState().EntityState.CooldownDuration;

                    // Reset animation speed
                    animator.speed = 1f;

                    OnActionEnded?.Invoke();

                    // Padding between action
                    yield return new WaitForSeconds(0.05f);
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    // Animation triggered function
    public void SignalAttack() {
        GetCurrentState().EntityState.OnDealingDamage();
    }

    public void LockMovement() {}
    public void UnlockMovement() {}

    // State management
    public void SetState(StateBinding state) {
        currentState = state;
    }

    public EntityStateBinding GetCurrentState() {
        return states[currentState];
    }
}
