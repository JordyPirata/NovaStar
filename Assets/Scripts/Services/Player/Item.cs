using Gameplay.Items;
using Player.Gameplay;
using UnityEngine;
using Services.Interfaces;

namespace Services.Player
{
    public class Item : InteractableObject
    {
        [ItemSelectorID, SerializeField] private int itemName;
        [SerializeField] private int amount;
        private string ItemData => ItemsUIConfiguration.Instance.items[itemName].itemName;

        public string ItemName => ItemsUIConfiguration.Instance.items[itemName].itemName;
        public override void Interact()
        {
            Debug.Log("interactuó");
            amount = ServiceLocator.GetService<IInventoryService>().TryPickItem(ItemData, amount);
            if (amount <= 0) Destroy(gameObject);
        }
    }
}