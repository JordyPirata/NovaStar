using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Items
{
    [Serializable]
    public class ItemData
    {
        public string itemName;
        public Sprite sprite;
        public int maxAmount;
    }
}