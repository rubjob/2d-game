using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Heal : BaseEntityState
    {
        [Header("Dependency")]
        public HealthScript HealthScript;

        [Header("Timing")]
        public float HealCooldown = 30f;
        public float AnimationDuration = 1f;

        [Header("Constant")]
        public float HealAmount = 25f;
        public int CurrentPotion = 3;
        public int MaxPotion = 3;

        public override float AttackSpeed => 1f / AnimationDuration;
        public override float CooldownDuration => HealCooldown;
        public override float AttackDamage => throw new System.NotImplementedException();
        public override HitboxManager Hitbox => throw new System.NotImplementedException();

        public override void OnDealingDamage() {
            throw new System.NotImplementedException();
        }

        public override IEnumerator OnPlayingAnimation() {
            if (CurrentPotion > 0) {
                yield return new WaitForSeconds(AnimationDuration);

                HealthScript.Heal(HealAmount);
                CurrentPotion -= 1;

                Debug.Log($"Potion left ({CurrentPotion}/{MaxPotion})");
            }
        }
    }
}