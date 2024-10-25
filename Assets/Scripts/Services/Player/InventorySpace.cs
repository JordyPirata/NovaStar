using System;
using Gameplay.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Services.Player
{
    public class InventorySpace : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private TextMeshProUGUI quantityText;
        [SerializeField] private Image itemImage;
        private bool _hasItem;
        private string _itemName;
        private int _quantity;
        private Action<InventorySpace, Sprite, RectTransform> _beginDrag;
        private Action<PointerEventData> _drag;
        private Action _endDrag;
        private ItemsUIConfiguration _itemsUIConfiguration;
        private RectTransform _rectTransform;

        public int Quantity
        {
            get => _quantity;
            private set
            {
                _quantity = value;
                quantityText.text = value != 0 ? _quantity.ToString() : string.Empty;
                if (value <= 0)
                {
                    itemImage.enabled = false;
                    quantityText.text = string.Empty;
                    _hasItem = false;
                }
            }
        }

        public bool HasItem => _hasItem;

        public string ItemName
        {
            get => _itemName;
            private set => _itemName = value;
        }

        public int PickItem(string itemName, int amount)
        {
            foreach (var itemUI in _itemsUIConfiguration.items)
            {
                if (itemUI.itemName == itemName)
                {
                    if (!_hasItem)
                    {
                        ItemName = itemName;
                        itemImage.sprite = itemUI.sprite;
                        itemImage.enabled = true;
                    }

                    _hasItem = true;
                    if (itemUI.maxAmount < amount + Quantity)
                    {
                        amount = amount + Quantity - itemUI.maxAmount;
                        Quantity = itemUI.maxAmount;
                        return amount;
                    }
                    else
                    {
                        Quantity += amount;
                        return 0;
                    }
                }
            }

            throw new Exception($"El item con el nombre {itemName} no se encuentra en la configuracion");
        }
        
        public int PickItem(InventorySpace item)
        {
            var amount = item.Quantity;
            if (HasItem && item.ItemName != ItemName)
                return amount;
            foreach (var itemUI in _itemsUIConfiguration.items)
            {
                if (itemUI.itemName == item.ItemName)
                {
                    if (!_hasItem)
                    {
                        ItemName = item.ItemName;
                        itemImage.sprite = itemUI.sprite;
                        itemImage.enabled = true;
                    }

                    _hasItem = true;
                    if (itemUI.maxAmount < amount + Quantity)
                    {
                        amount = amount + Quantity - itemUI.maxAmount;
                        Quantity = itemUI.maxAmount;
                        return amount;
                    }
                    else
                    {
                        Quantity += amount;
                        return 0;
                    }
                }
            }

            throw new Exception($"El item con el nombre {item.ItemName} no se encuentra en la configuracion");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_hasItem) return;
            _beginDrag?.Invoke(this, itemImage.sprite, _rectTransform);
            Debug.Log("beginDrag");
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_hasItem) return;
            _drag?.Invoke(eventData);
            Debug.Log("drag");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _endDrag?.Invoke();
            Debug.Log("endDrag");
        }

        public void OnDrop(PointerEventData eventData)
        {
            var droppedObject = eventData.pointerDrag;
            if (droppedObject != null)
            {
                if (droppedObject.TryGetComponent<InventorySpace>(out var inventorySpace))
                {
                    if (!inventorySpace.HasItem || inventorySpace == this) return;
                    inventorySpace.Quantity = PickItem(inventorySpace);
                }
                Debug.Log($"Dropped {eventData.pointerDrag.name} into {gameObject.name}");
            }
        }


        public void Configure(ItemsUIConfiguration itemsUIConfiguration, Action<InventorySpace, Sprite, RectTransform> beginDrag, Action<PointerEventData> drag, Action endDrag)
        {
            _beginDrag += beginDrag;
            _drag += drag;
            _endDrag += endDrag;
            _itemsUIConfiguration = itemsUIConfiguration;
            _rectTransform = GetComponent<RectTransform>();
        }

        public int CanSaveItem(ItemData item, int amount)
        {
            foreach (var itemUI in _itemsUIConfiguration.items)
            {
                if (itemUI.itemName == item.itemName)
                {
                    if (itemUI.maxAmount < amount + Quantity)
                    {
                        amount = amount + Quantity - itemUI.maxAmount;
                        return amount;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            throw new Exception($"El item con el nombre {item.itemName} no se encuentra en la configuracion");
        }

        public int DiscardItem(string itemName, int quantity)
        {
            foreach (var itemUI in _itemsUIConfiguration.items)
            {
                if (itemUI.itemName == itemName)
                {
                    if (quantity < Quantity)
                    {
                        Quantity -= quantity;
                        return 0;
                    }
                    else
                    {
                        var restingQuantity = quantity - Quantity;
                        Quantity = 0;
                        ItemName = string.Empty;
                        itemImage.sprite = null;
                        itemImage.enabled = false;
                        _hasItem = false;
                        return restingQuantity;
                    }
                }
            }
            throw new Exception($"El item con el nombre {itemName} no se encuentra en la configuracion");
        }
    }
}