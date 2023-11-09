using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory {
    public class ItemStack {
        public Item Item;
        public int Count;
    }

    public class Inventory : MonoBehaviour {
        public readonly static int MaxActiveSlots = 4;

        [Header("Keybindings")]
        public InputActionReference UseItemKey;

        private List<ItemStack> Backpacks = new();
        private int[] ActiveSlots = new int[MaxActiveSlots];
        private int SelectedSlot = 0;

        private readonly Dictionary<int, float> ItemLastUsed = new();

        private void Update() {
            if (UseItemKey.action.IsInProgress() &&
                ItemLastUsed[GetItemStackIndex()] + GetItemStackInActiveSlots().Item.Cooldown < Time.time) {

                ItemLastUsed[GetItemStackIndex()] = Time.time;

                foreach (ItemEffect e in GetItemStackInActiveSlots().Item.Effects) {
                    Debug.Log(e.Type + ": " + e.Amount);
                }
            }
        }

        public ItemStack GetItemStackInActiveSlots() {
            return Backpacks[ActiveSlots[SelectedSlot]];
        }

        public int GetItemStackIndex() {
            return ActiveSlots[SelectedSlot];
        }

        public void AddItem(Item item, int count) {
            foreach (ItemStack s in Backpacks) {
                if (s.Item == item) {
                    s.Count = (int) Mathf.Clamp(s.Count + count, 0, s.Item.MaxItemStack);
                    return;
                }
            }

            ItemStack st = new();
            st.Item = item;
            st.Count = count;

            Backpacks.Add(st);
        }
    }
}
