using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

namespace Gameplay.Items
{
    [CreateAssetMenu (fileName = "ItemsUIConfiguration", menuName = "NovaStar/ItemsUIConfiguration")]
    public class ItemsUIConfiguration : ScriptableObject
    {
    [SerializeField] public ItemData[] items;
    [SerializeField] public List<RarityColor> rarityColors;

        private static ItemsUIConfiguration m_Data;
        public static ItemsUIConfiguration Instance
        {
            get
            {
                if (m_Data == null)
                {
                    m_Data = Resources.Load("ItemsUIConfiguration", typeof(ItemsUIConfiguration)) as ItemsUIConfiguration;
                }
                return m_Data;
            }
        }

        [Serializable]
        public class RarityColor
        {
            public ItemRarity itemRarity;
            public Color color;
        }

        public Color GetColorByRarity(ItemRarity itemRarity)
        {
            foreach (var color in rarityColors.Where(color => color.itemRarity == itemRarity))
            {
                return color.color;
            }

            throw new Exception($"The rarity {itemRarity.ToString()} doesnt have color asigned");
        }

        public string[] GetAllItemNames()
        {
            return items.ToList().Select(x => x.itemName).ToArray();
        }
    }
}