using System;
using System.Collections.Generic;
using Player.Gameplay;
using UnityEngine;

namespace Gameplay.Items.Crafting
{
    [Serializable]
    public class CraftingRecipe
    {
        [ItemSelectorID] public int craftedItem;
        public List<CraftingIngredient> craftingIngredients;
    }
}