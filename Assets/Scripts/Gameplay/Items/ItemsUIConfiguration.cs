using System;
using System.Collections.Generic;
using System.Linq;
using Services.Player;
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

        public int GetItemById(string itemId)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].itemName == itemId)
                    return i;
            }
            throw new Exception($"El item con el nombre {itemId} no se encuentra en la configuracion");
        }
        
        public ItemData GetItemDataById(string itemId)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].itemName == itemId)
                    return items[i];
            }
            throw new Exception($"El item con el nombre {itemId} no se encuentra en la configuracion");
        }

    }
}