using System;
using System.Collections.Generic;
using Gameplay.Items;

namespace Player.Gameplay
{
    [Serializable]
    public class DropsByRarity
    {
        public ItemRarity rarity;
        [ItemSelectorID] public List<int> drops;
    }
}