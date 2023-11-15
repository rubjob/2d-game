using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.Events;

public class ActionManager : MonoBehaviour
{
    [Header("Animation")]
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Entity States")]
    public BindingState currentState;
    public EntityStateBinding[] stateBindings;

    private Dictionary<BindingState, EntityStateBinding> states = new();
    public Dictionary<BindingState, EntityStateBinding> SkillStates { get => states; }

    private Dictionary<BindingState, float> cooldowns = new();
    public Dictionary<BindingState, float> SkillCooldowns { get => cooldowns; }

    [Header("Mouse Util")]
    public MouseUtil MouseUtil;

    [Header("Events")]
    public UnityEvent OnActionStarting;
    public UnityEvent OnActionEnded;

    public bool IsUsingUltimate { private set; get; } = false;
    private float ByPassedCooldown = 1f;

    private void Start()
    {
        foreach (EntityStateBinding e in stateBindings)
            states.Add(e.StateBinding, e);

        foreach (EntityStateBinding e in stateBindings)
            cooldowns.Add(e.StateBinding, 0);

        StartCoroutine(DetectAction());
    }

    // Co-routine for detecting input and executing its action. This runs in parallel in background.
    private IEnumerator DetectAction()
    {
        while (true)
        {
            foreach (KeyValuePair<BindingState, EntityStateBinding> e in states)
            {
                if (e.Value.InputBinding.action.IsInProgress() && cooldowns[e.Key] <= Time.time)
                {
                    SetState(e.Key);

                    OnActionStarting?.Invoke();

                    // Stop movement on starting action
                    if (GetCurrentState().StopOnActionStarting)
                        rb.velocity = Vector2.zero;

                    // Set animation direction
                    switch (GetCurrentState().Focus)
                    {
                        case Focus.None: break;
                        case Focus.Movement: break;
                        case Focus.PointerBidirectional: FocusPointerBidirectional(); break;
                        case Focus.Pointer: FocusPointer(); break;
                    }

                    // Play animation and wait for return
                    yield return StartCoroutine(GetCurrentState().EntityState.OnPlayingAnimation());

                    // Set cooldown
                    cooldowns[e.Key] = Time.time + GetSkillCooldown(e.Key);

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

    public float GetSkillCooldown(BindingState state)
    {
        return ((IsUsingUltimate && (state == BindingState.QAttack || state == BindingState.EAttack)) ?
                        ByPassedCooldown : states[state].EntityState.CooldownDuration);
    }

    private void FocusPointerBidirectional()
    {
        HitboxManager hitbox = GetCurrentState().EntityState.Hitbox;
        float mAngle = MouseUtil.GetMouseAngle();

        hitbox.FlipX(Mathf.Abs(mAngle) > 90f);
        spriteRenderer.flipX = Mathf.Abs(mAngle) > 90f;
    }

    private void FocusPointer()
    {
        HitboxManager hitbox = GetCurrentState().EntityState.Hitbox;
        float mAngle = MouseUtil.GetMouseAngle();

        hitbox.RotateTo(mAngle);

        if (mAngle <= 45 && mAngle >= -45)
        {
            //attack right animation
            spriteRenderer.flipX = false;
            animator.SetTrigger("isAttackingSide");
        }
        else if (mAngle >= 135 || mAngle <= -135)
        {
            //attack left animation
            spriteRenderer.flipX = true;
            animator.SetTrigger("isAttackingSide");
        }
        else if (mAngle < -45 && mAngle > -135)
        {
            //attack down animation
            animator.SetTrigger("isAttackingDown");
        }
        else if (mAngle > 45 && mAngle < 135)
        {
            //attack up animation
            animator.SetTrigger("isAttackingUp");
        }
    }

    // Animation triggered function
    public void SignalAttack()
    {
        GetCurrentState().EntityState.OnDealingDamage();
    }

    public void LockMovement() { }
    public void UnlockMovement() { }

    // State
    public void SetState(BindingState state)
    {
        currentState = state;
    }

    public EntityStateBinding GetCurrentState()
    {
        return states[currentState];
    }

    // Cooldown bypass
    public void SetCooldownByPass(bool val, float byPassedCooldown = 1f)
    {
        this.IsUsingUltimate = val;
        this.ByPassedCooldown = byPassedCooldown;
    }
    public void TriggerIFrame()
    {
        Physics2D.IgnoreLayerCollision(3, 6, true);
        GetComponent<HealthScript>().isInvulnerable = true;
    }
    public void RemoveIFrame()
    {
        Physics2D.IgnoreLayerCollision(3, 6, false);
        GetComponent<HealthScript>().isInvulnerable = false;
    }
}
