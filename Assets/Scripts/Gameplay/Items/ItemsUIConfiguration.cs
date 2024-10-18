using System.Linq;
using UnityEngine;

namespace Gameplay.Items
{
    [CreateAssetMenu (fileName = "ItemsUIConfiguration", menuName = "NovaStar/ItemsUIConfiguration")]
    public class ItemsUIConfiguration : ScriptableObject
    {
    [SerializeField] public ItemData[] items;
        
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

        public string[] GetAllItemNames()
        {
            return items.ToList().Select(x => x.itemName).ToArray();
        }
    }
}