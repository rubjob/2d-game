using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public abstract class BaseEntityState : MonoBehaviour {
    public abstract float BaseAttackDamage { get; }
    public float AttackDamage { get => BaseAttackDamage + AdditionalAttackDamage; }
    public float AdditionalAttackDamage { get; set; } = 0f;

    public abstract float BaseAttackSpeed { get; }
    public float AttackSpeed { get => BaseAttackSpeed + AdditionalAttackSpeed; }
    public float AdditionalAttackSpeed { get; set; } = 0f;

    public abstract float CooldownDuration { get; }
    public abstract HitboxManager Hitbox { get; }

    public abstract IEnumerator OnPlayingAnimation();
    public abstract void OnDealingDamage();
}