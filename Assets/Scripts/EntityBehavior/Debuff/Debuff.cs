using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EntityBehavior
{
    [CreateAssetMenu(menuName = "Debuff")]
    public class Debuff : ScriptableObject
    {
        public string Name;
        public float Duration;
        public float TriggerEvery;
        public float Damage;
    }
}
