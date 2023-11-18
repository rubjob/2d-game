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

        public Item Item;

        [Header("Keybindings")]
        public InputActionReference UseItemKey;

        private List<ItemStack> Backpacks = new();
        private int[] ActiveSlots = new int[MaxActiveSlots];
        private int SelectedSlot = 0;

        private readonly Dictionary<int, float> NextUseTime = new();

        private void Start() {
            for (int i = 0; i < ActiveSlots.Length; i++) ActiveSlots[i] = -1;

            ItemStack itemSt = new ItemStack();

            itemSt.Item = Item;
            itemSt.Count = 1;

            Backpacks.Add(itemSt);
            ActiveSlots[0] = 0;
            NextUseTime[0] = 0;
        }

        private void Update() {
            if (UseItemKey.action.IsInProgress()) UseItem();
        }

        private void UseItem() {
            if (GetItemStackInActiveSlots() != null && NextUseTime[GetItemStackIndex()] < Time.time) {
                NextUseTime[GetItemStackIndex()] = Time.time + GetItemStackInActiveSlots().Item.Cooldown;

                foreach (ItemEffect e in GetItemStackInActiveSlots().Item.Effects) {
                    Debug.Log(e.Type + ": " + e.Amount);
                }

                ItemStack currentItem = GetItemStackInActiveSlots();
                currentItem.Count--;

                if (currentItem.Count == 0) {
                    ActiveSlots[GetItemStackIndex()] = -1;
                    NextUseTime.Remove(GetItemStackIndex());
                    Backpacks.Remove(currentItem);
                }
            }
        }

        public ItemStack GetItemStackInActiveSlots() {
            int index = GetItemStackIndex();
            return (index == -1) ? null : Backpacks[index];
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
