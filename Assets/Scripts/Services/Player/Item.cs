using Gameplay.Items;
using UnityEngine;

namespace Services.Player
{
    public class Item : InteractableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private int amount;

        public string ItemName => itemName;
        public override void Interact()
        {
            Debug.Log("interactuó");
            amount = ServiceLocator.GetService<IInventoryService>().TryPickItem(this, amount);
            if (amount <= 0) Destroy(gameObject);
        }
    }
}