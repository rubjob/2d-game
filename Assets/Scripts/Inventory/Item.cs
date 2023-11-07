using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Inventory {
    public enum ItemEffectType {
        Health,
        MaxHealth,
        AttackDamage,
        AttackSpeed,
        MovementSpeed,
    }

    [Serializable]
    public class ItemEffect {
        public ItemEffectType Type;
        public float Amount;
    }

    [CreateAssetMenu(menuName="Item")]
    public class Item : ScriptableObject {
        public string Name;
        public string Description;
        public ItemEffect[] Boosts;
    }
}
