using System;
using System.Collections.Generic;
using Gameplay.Items;
using Player.Gameplay;

namespace Services.Player
{
    [Serializable]
    public class DropsByRarity
    {
        public ItemRarity rarity;
        [ItemSelectorID] public List<int> drops;
    }
}