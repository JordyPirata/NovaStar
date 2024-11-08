using System;
using Player.Gameplay;
using UnityEngine;

namespace Gameplay.Items.Crafting
{
    [Serializable]
    public class CraftingIngredient
    {
        [ItemSelectorID, SerializeField] private int item;
        public int quantity;
        public ItemData Item => ItemsUIConfiguration.Instance.items[item];
    }
}