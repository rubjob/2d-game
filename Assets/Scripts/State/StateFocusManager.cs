using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateFocus {
    None,
    Pointer,
    PointerBidirectional,
    Movement,
}

[Serializable]
public class StateFocusBinding {
    public StateBinding StateBinding;
    public StateFocus Focus;
}


public class StateFocusManager : MonoBehaviour
{
    [Header("Dependency")]
    public MouseUtil MouseUtil;
    public SpriteRenderer SpriteRenderer;
    public Animator Animator;
    public ActionManager ActionManager;

    [Header("Focus")]
    public StateFocusBinding[] FocusBindings;

    private Dictionary<StateBinding, StateFocusBinding> MappedFocusBindings = new();

    private void Start() {
        foreach (StateFocusBinding e in FocusBindings)
            MappedFocusBindings.Add(e.StateBinding, e);
    }

    public void SetStateFocus(StateBinding state) {
        if (MappedFocusBindings[state] == null) return;

        switch (MappedFocusBindings[state].Focus) {
            case StateFocus.None: break;
            case StateFocus.Movement: break;
            case StateFocus.PointerBidirectional: FocusPointerBidirectional(); break;
            case StateFocus.Pointer: FocusPointer(); break;
        }
    }

    private void FocusPointerBidirectional() {
        HitboxManager hitbox = ActionManager.GetCurrentState().EntityState.Hitbox;
        float mAngle = MouseUtil.GetMouseAngle();

        hitbox.FlipX(Mathf.Abs(mAngle) > 90f);
        SpriteRenderer.flipX = Mathf.Abs(mAngle) > 90f;
    }

    private void FocusPointer() {
        HitboxManager hitbox = ActionManager.GetCurrentState().EntityState.Hitbox;
        float mAngle = MouseUtil.GetMouseAngle();

        hitbox.RotateTo(mAngle);

        if (mAngle <= 45 && mAngle >= -45) {
            //attack right animation
            SpriteRenderer.flipX = false;
            Animator.SetTrigger("isAttackingSide");
        }
        else if (mAngle >= 135 || mAngle <= -135) {
            //attack left animation
            SpriteRenderer.flipX = true;
            Animator.SetTrigger("isAttackingSide");
        }
        else if (mAngle < -45 && mAngle > -135) {
            //attack down animation
            Animator.SetTrigger("isAttackingDown");
        }
        else if (mAngle > 45 && mAngle < 135) {
            //attack up animation
            Animator.SetTrigger("isAttackingUp");
        }
    }
}
