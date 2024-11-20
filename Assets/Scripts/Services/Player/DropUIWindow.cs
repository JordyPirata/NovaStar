using System;
using System.Collections.Generic;
using Gameplay.Items;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services.Player
{
    public class DropUIWindow : MonoBehaviour
    {
        [SerializeField] private List<InventorySpace> inventorySpaces;
        [SerializeField] private MovingInventorySpace movingInventorySpace;

        private void Awake()
        {
            foreach (var inventorySpace in inventorySpaces)
            {
                inventorySpace.Configure(BeginDrag, Drag, EndDrag);
            }
        }

        private void BeginDrag(InventorySpace inventorySpaceDragging, Sprite itemSprite, RectTransform rectTransform)
        {
            movingInventorySpace.RectTransform.anchoredPosition = rectTransform.anchoredPosition;
            movingInventorySpace.Configure(inventorySpaceDragging, itemSprite);
            movingInventorySpace.gameObject.SetActive(true);
        }

        private void Drag(PointerEventData eventData)
        {
            movingInventorySpace.RectTransform.position = eventData.position;
        }

        private void EndDrag()
        {
            movingInventorySpace.gameObject.SetActive(false);
        }

        public void OpenDrop(List<int> droppedItemsIndex)
        {
            if (ServiceLocator.GetService<IUIService>().OpenUIPanel(UIPanelType.Drops))
            {
                foreach (var itemIndex in droppedItemsIndex)
                {
                    TryPickItem(ItemsUIConfiguration.Instance.items[itemIndex].itemName, 1);
                }
            }
        }

        public void CloseDrop()
        {
            ServiceLocator.GetService<IUIService>().OpenUIPanel(UIPanelType.Drops);
        }
        
        public int TryPickItem (string itemName, int quantity)
        {
            var startingQuantity = quantity;
            foreach (var inventorySpace in inventorySpaces)
            {
                if (!inventorySpace.HasItem || itemName == inventorySpace.ItemName)
                {
                    quantity = inventorySpace.PickItem(itemName, quantity);
                    if (quantity <= 0) break; 
                }
            }

            if (quantity == startingQuantity)
            {
                return quantity;
            }
            return quantity;
        }

        public void OnPickAll()
        {
            foreach (var inventorySpace in inventorySpaces)
            {
                ServiceLocator.GetService<IInventoryService>().TryPickItem(inventorySpace.ItemName, inventorySpace.Quantity);
            }
        }
    }
}