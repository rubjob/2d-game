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

    [Header("Mouse Util")]
    public MouseUtil MouseUtil;

    [Header("Events")]
    public UnityEvent OnActionStarting;
    public UnityEvent OnActionEnded;

    private void Start() {
        foreach (EntityStateBinding e in stateBindings)
            states.Add(e.StateBinding, e);

        StartCoroutine(DetectAction());
    }

    private void Update() {
        UpdateHitBox();
    }

    // Co-routine for detecting input and executing its action. This runs in parallel in background.
    private IEnumerator DetectAction() {
        while (true) {
            foreach (KeyValuePair<BindingState, EntityStateBinding> e in states) {
                if (e.Value.InputBinding.action.IsInProgress()) {
                    SetState(e.Key);

                    OnActionStarting?.Invoke();

                    if (GetCurrentState().FocusPointer)
                        SetAnimationDirection();

                    // Play animation and wait for return
                    yield return StartCoroutine(GetCurrentState().EntityState.OnPlayingAnimation());

                    animator.speed = 1f;
                    OnActionEnded?.Invoke();

                    // Padding between action
                    yield return new WaitForSeconds(0.05f);
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void SetAnimationDirection() {
        float mAngle = MouseUtil.GetMouseAngle();
        if (mAngle <= 45 && mAngle >= -45) {
            //attack right animation
            spriteRenderer.flipX = false;
            animator.SetTrigger("isAttackingSide");
        }
        else if (mAngle >= 135 || mAngle <= -135) {
            //attack left animation
            spriteRenderer.flipX = true;
            animator.SetTrigger("isAttackingSide");
        }
        else if (mAngle < -45 && mAngle > -135) {
            //attack down animation
            animator.SetTrigger("isAttackingDown");
        }
        else if (mAngle > 45 && mAngle < 135) {
            //attack up animation
            animator.SetTrigger("isAttackingUp");
        }
    }

    // Animation triggered function
    public void SignalAttack() {
        GetCurrentState().EntityState.OnDealingDamage();
    }

    public void LockMovement() {

    }

    public void UnlockMovement() {

    }

    // State
    public void SetState(BindingState state) {
        currentState = state;
    }

    public EntityStateBinding GetCurrentState() {
        return states[currentState];
    }

    // Combat
    public void UpdateHitBox() {
        float angle = MouseUtil.GetMouseAngle();
        foreach (KeyValuePair<BindingState, EntityStateBinding> e in states) {
            if (e.Value.EntityState.Hitbox != null)
                e.Value.EntityState.Hitbox.RotateTo(angle);
        }
    }
}
