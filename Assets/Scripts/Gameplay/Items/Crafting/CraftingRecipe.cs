using System;
using System.Collections.Generic;
using Player.Gameplay;
using UnityEngine;

namespace Gameplay.Items.Crafting
{
    [Serializable]
    public class CraftingRecipe
    {
        [ItemSelectorID, SerializeField] private int craftedItem;
        public int craftedQuantity;
        public List<CraftingIngredient> craftingIngredients;

        public ItemData CraftedItem => ItemsUIConfiguration.Instance.items[craftedItem];
    }
}