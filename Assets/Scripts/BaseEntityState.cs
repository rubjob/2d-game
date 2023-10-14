using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntityState : MonoBehaviour {
    public abstract float AttackDamage { get; }
    public abstract float AttackSpeed { get; }
    public abstract float CooldownDuration { get; }
    public abstract HitboxManager Hitbox { get; }
    public abstract bool StateSignal { get; }

    public abstract IEnumerator OnPlayingAnimation();
    public abstract void OnDealingDamage();
}