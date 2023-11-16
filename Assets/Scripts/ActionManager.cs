using System;
using System.Collections;
using System.Collections.Generic;
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

    public Dictionary<BindingState, EntityStateBinding> SkillStates { get; } = new();
    public Dictionary<BindingState, float> SkillCooldowns { get; } = new();

    [Header("Mouse Util")]
    public MouseUtil MouseUtil;

    [Header("Events")]
    public UnityEvent OnActionStarting;
    public UnityEvent OnActionEnded;

    public bool IsUsingUltimate { private set; get; } = false;
    private float ByPassedCooldown = 1f;
    private float AdditionalAttackDamage = 0f;
    private float AdditionalAttackSpeed = 0f;

    private void Start()
    {
        foreach (EntityStateBinding e in stateBindings)
            SkillStates.Add(e.StateBinding, e);

        foreach (EntityStateBinding e in stateBindings)
            SkillCooldowns.Add(e.StateBinding, 0);

        StartCoroutine(DetectAction());
    }

    // Co-routine for detecting input and executing its action. This runs in parallel in background.
    private IEnumerator DetectAction()
    {
        while (true)
        {
            foreach (KeyValuePair<BindingState, EntityStateBinding> e in SkillStates)
            {
                if (e.Value.InputBinding.action.IsInProgress() && SkillCooldowns[e.Key] <= Time.time)
                {
                    SetState(e.Key);

                    OnActionStarting?.Invoke();

                    // Set additional attack speed, attack damage
                    if (e.Key == BindingState.PrimaryAttack || e.Key == BindingState.HeavyAttack) {
                        GetCurrentState().EntityState.AdditionalAttackDamage = AdditionalAttackDamage;
                        GetCurrentState().EntityState.AdditionalAttackSpeed = AdditionalAttackSpeed;
                    }

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
                    SkillCooldowns[e.Key] = Time.time + GetSkillCooldown(e.Key);

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

    public void SetAdditionalAttackSpeed(float Amount) {
        AdditionalAttackSpeed = Amount;
    }

    public void SetAdditionalAttackDamage(float Amount) {
        AdditionalAttackDamage = Amount;
    }

    public float GetSkillCooldown(BindingState state)
    {
        return (IsUsingUltimate && (state == BindingState.QAttack || state == BindingState.EAttack)) ?
                        ByPassedCooldown : SkillStates[state].EntityState.CooldownDuration;
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
        return SkillStates[currentState];
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
