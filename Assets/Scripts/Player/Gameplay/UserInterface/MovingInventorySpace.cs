using Services.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Gameplay.UserInterface
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