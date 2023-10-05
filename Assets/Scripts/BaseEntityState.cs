using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntityState : MonoBehaviour {
    public abstract float AttackDamage { get; }
    public abstract float AttackSpeed { get; }
    public abstract HitboxManager Hitbox { get; }

    public abstract IEnumerator OnPlayingAnimation();
    public abstract void OnDealingDamage();
}