using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityBehavior
{
    public enum StatusApplyingType
    {
        Instant,
        Interval,
        Continuous,
    }

    [CreateAssetMenu(menuName = "Status/Status Effect")]
    public class Status : ScriptableObject
    {
        [Header("Generic Information")]
        public string Name;
        public Sprite Image;

        [Header("Timing")]
        public StatusApplyingType Type;
        public float Duration;
        public float TriggerEvery;

        [Header("Buff")]
        public float AttackSpeedBoost;
        public float AttackDamageBoost;
        public float HealthBoost;
        public float MaxHealthBoost;
        public float SpeedBoost;

        [Header("Debuff")]
        public float Damage;
    }
}