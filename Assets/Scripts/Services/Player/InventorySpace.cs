using System;
using Gameplay.Items;
using UnityEngine;

namespace Services.Player
{
    public class InventorySpace : MonoBehaviour
    {
        private bool _hasItem;
        private string _itemName;
        private int _quantity;

        public bool HasItem => _hasItem;
        public string ItemName => _itemName;

        public int PickItem(Item item, int amount, ItemsUIConfiguration itemsUIConfiguration)
        {
            _hasItem = true;
            foreach (var itemUI in itemsUIConfiguration.items)
            {
                if (itemUI.itemName == item.name)
                {
                    if (itemUI.maxAmount < amount)
                    {
                        _quantity = itemUI.maxAmount;
                        return amount - itemUI.maxAmount;
                    }
                }
                else
                {
                    _quantity = amount;
                    return 0;
                }
            }

            throw new Exception($"El item con el nombre {item.name} no se encuentra en la configuracion");
        }
    }
}