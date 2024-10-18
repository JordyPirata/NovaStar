using System;
using Player.Gameplay;

namespace Gameplay.Items.Crafting
{
    [Serializable]
    public class CraftingIngredient
    {
        [ItemSelectorID] public int item;
        public int quantity;
    }
}