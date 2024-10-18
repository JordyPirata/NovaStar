using System;
using Gameplay.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Services.Player
{
    public class MovingInventorySpace : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI quantityText;
        [SerializeField] private Image itemImage;
        [SerializeField] private RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform;


        public void Configure(InventorySpace inventorySpaceDragging, Sprite itemSprite)
        {
            quantityText.text = inventorySpaceDragging.Quantity.ToString();
            itemImage.sprite = itemSprite;
        }
    }
}