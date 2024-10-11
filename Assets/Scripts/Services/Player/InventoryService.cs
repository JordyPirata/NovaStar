using System.Collections.Generic;
using Gameplay.Items;
using Services.Interfaces;
using UnityEngine;

namespace Services.Player
{
    public class InventoryService : MonoBehaviour, IInventoryService
    {
        [SerializeField] private List<InventorySpace> inventorySpaces;
        [SerializeField] private ItemsUIConfiguration itemsUIConfiguration;
        public int TryPickItem (Item item, int quantity)
        {
            foreach (var inventorySpace in inventorySpaces)
            {
                if (!inventorySpace.HasItem || item.name == inventorySpace.ItemName)
                {
                    quantity = inventorySpace.PickItem(item, quantity, itemsUIConfiguration);
                    if (quantity == 0)
                        return quantity;
                }
            }
            return quantity;
        }
    }
}