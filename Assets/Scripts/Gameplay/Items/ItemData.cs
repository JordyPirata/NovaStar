using System;
using Services.Interfaces;
using Services.Player;
using UnityEngine;
using UnityEngine.Events;
using Util;

namespace Gameplay.Items
{
    [Serializable]
    public class ItemData
    {
        public string itemName;
        public Sprite sprite;
        public int maxAmount;
        public ItemRarity itemRarity = ItemRarity.Common;
        public bool isEquipable, isConsumable;
        public ConsumableType consumableType;

        public bool Consume()
        {
            return ServiceLocator.GetService<IPlayerMediator>().UseConsumable(consumableType);
        }
    }
}