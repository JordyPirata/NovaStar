using System;
using System.Collections.Generic;
using Gameplay.Items;
using InputSystem;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Services.Player
{
    public class InventoryService : MonoBehaviour, IInventoryService
    {
        [SerializeField] private List<InventorySpace> inventorySpaces;
        [SerializeField] private ItemsUIConfiguration itemsUIConfiguration;
        [SerializeField] private GameObject panelGameObject;
        [SerializeField] private MovingInventorySpace movingInventorySpace;
        private Dictionary<string, int> _completeInventory;
        private InputActions _inputActions;
        private bool _open;

        private void Awake()
        {
            _inputActions = ServiceLocator.GetService<IInputActions>().InputActions;
            _inputActions.Player.InventoryMenu.performed += OnInventoryMenu;
            panelGameObject.SetActive(false);
            foreach (var inventorySpace in inventorySpaces)
            {
                inventorySpace.Configure(itemsUIConfiguration, BeginDrag, Drag, EndDrag);
            }
        }

        private void OnInventoryMenu(InputAction.CallbackContext obj)
        {
            _open = !_open;
            Cursor.visible = _open;
            panelGameObject.SetActive(_open);
            Cursor.lockState = _open ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public int TryPickItem (Item item, int quantity)
        {
            var startingQuantity = quantity;
            foreach (var inventorySpace in inventorySpaces)
            {
                if (!inventorySpace.HasItem || item.ItemName == inventorySpace.ItemName)
                {
                    quantity = inventorySpace.PickItem(item, quantity);
                    if (quantity == 0)
                        return quantity;
                }
            }

            if (_completeInventory.ContainsKey(item.ItemName))
            {
                _completeInventory[item.ItemName] += startingQuantity - quantity;
                Debug.Log($"Now you have {_completeInventory[item.ItemName]} units of {item.ItemName}");
            }
            return quantity;
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
    }
}