using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Items;
using Gameplay.Items.Crafting;
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
        [SerializeField] private MovingInventorySpace movingInventorySpace;
        [SerializeField] public Equipable[] equipables;
        private Dictionary<string, int> _completeInventory;
        private InputActions _inputActions;
        private Action<Dictionary<string, int>> onInventoryUpdate;
        private bool _hasTool;

        private void Awake()
        {
            _completeInventory = new Dictionary<string, int>();
            foreach (var inventorySpace in inventorySpaces)
            {
                inventorySpace.Configure(BeginDrag, Drag, EndDrag);
                inventorySpace.OnEquipItem += EquipItem;
                inventorySpace.OnUnEquipItem += UnEquipItem;
            }

            TryPickItem("Herramienta de recoleccion", 1);
            TryPickItem("Tableta de teletransporte", 1);
            TryPickItem("Linterna", 1);
        }
        
        public void CanGetItems(bool canGetItems)
        {
            _hasTool = canGetItems;
        }

        public int TryPickItem (string itemName, int quantity, bool needsTool = false)
        {
            if (needsTool && !_hasTool) return quantity;
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
            if (_completeInventory.ContainsKey(itemName))
            {
                _completeInventory[itemName] += startingQuantity - quantity;
                Debug.Log($"Now you have {_completeInventory[itemName]} units of {itemName}");
            }
            else
            {
                _completeInventory.Add(itemName, startingQuantity - quantity);
                Debug.Log($"Now you have {_completeInventory[itemName]} units of {itemName}");
            }
            onInventoryUpdate?.Invoke(_completeInventory);
            return quantity;
        }
        
        private bool CanSaveItem (ItemData item, int quantity)
        {
            var startingQuantity = quantity;
            foreach (var inventorySpace in inventorySpaces)
            {
                if (!inventorySpace.HasItem || item.itemName == inventorySpace.ItemName)
                {
                    quantity = inventorySpace.CanSaveItem(item, quantity);
                }
            }

            if (quantity == startingQuantity)
            {
                return false;
            }
            onInventoryUpdate?.Invoke(_completeInventory);
            if (quantity == 0)
            {
                return true;
            }

            if (quantity > 0)
            {
                return false;
            }
            throw new Exception("Trying save item got negative remainder");
        }

        public ref Action<Dictionary<string, int>> GetOnInventoryUpdated()
        {
            return ref onInventoryUpdate;
        }

        public void TryCraftItem(CraftingRecipe recipe)
        {
            if (CanSaveItem(recipe.CraftedItem, recipe.craftedQuantity)) ;
            {
                var restingItems = TryPickItem(recipe.CraftedItem.itemName, recipe.craftedQuantity);
                if (restingItems != 0)
                {
                    Debug.LogError($"at crafting item {recipe.CraftedItem.itemName} got {restingItems} resting items");
                }

                foreach (var recipeCraftingIngredient in recipe.craftingIngredients)
                {
                    var restingDiscardedItems = TryDiscardItems(recipeCraftingIngredient.Item.itemName,
                        recipeCraftingIngredient.quantity);
                    if (restingDiscardedItems != 0)
                        Debug.LogError(
                            $"at discarding item {recipeCraftingIngredient.Item.itemName} got {restingDiscardedItems} resting items");
                    
                }
            }
        }

        public int TryDiscardItems(string itemName, int quantity)
        {
            var startingQuantity = quantity;
            foreach (var inventorySpace in inventorySpaces)
            {
                if (inventorySpace.HasItem && itemName == inventorySpace.ItemName)
                {
                    quantity = inventorySpace.DiscardItem(itemName, quantity);
                }
            }

            if (quantity == startingQuantity)
            {
                return quantity;
            }
            if (_completeInventory.ContainsKey(itemName))
            {
                _completeInventory[itemName] -= startingQuantity - quantity;
                Debug.Log($"Now you have {_completeInventory[itemName]} units of {itemName}");
            }
            else
            {
                Debug.LogError($"Tried to remove {itemName} from inventoryDictionary but doesnt contains the key");
            }

            if (_completeInventory[itemName] < 0)
                Debug.LogError($"inventoryDictionary contains {_completeInventory[itemName]} units of {itemName}");
            if (_completeInventory[itemName] == 0)
                _completeInventory.Remove(itemName);
            onInventoryUpdate?.Invoke(_completeInventory);
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

        private void EquipItem(string itemId)
        {
            int itemIntId = itemsUIConfiguration.GetItemById(itemId);
            foreach (var equipable in equipables.Where(equipable=>equipable.CorrespondentItem == itemIntId))
            {
                equipable.gameObject.SetActive(true);
                equipable.Equip();
            }
        }

        private void UnEquipItem(string itemId)
        {
            int itemIntId = itemsUIConfiguration.GetItemById(itemId);
            foreach (var equipable in equipables.Where(equipable=>equipable.CorrespondentItem == itemIntId))
            {
                equipable.gameObject.SetActive(false);
                equipable.UnEquip();
            }
        }
    }
}