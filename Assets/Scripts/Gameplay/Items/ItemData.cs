using System;
using Services.Player;
using UnityEngine;

namespace Gameplay.Items
{
    [Serializable]
    public class ItemData
    {
        public string itemName;
        public Sprite sprite;
        public int maxAmount;
        public ItemRarity itemRarity = ItemRarity.Common;
        public bool isEquipable;
        public Equipable equipableReference;
    }
}