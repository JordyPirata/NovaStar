using System;
using UnityEngine;

namespace Gameplay.Items
{
    [CreateAssetMenu (fileName = "ItemsUIConfiguration", menuName = "NovaStarItemsUIConfiguration")]
    public class ItemsUIConfiguration : ScriptableObject
    {
        [Serializable]
        public class ItemData
        {
            public string itemName;
            public Sprite sprite;
            public int maxAmount;
        }
        
        [SerializeField] public ItemData[] items;
    }
}