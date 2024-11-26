using Player.Gameplay.UserInterface;
using Services.Interfaces;
using Services.Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class PickItemArea : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            var droppedObject = eventData.pointerDrag;
            if (droppedObject != null)
            {
                if (droppedObject.TryGetComponent<InventorySpace>(out var inventorySpace))
                {
                    if (!inventorySpace.HasItem) return;
                    inventorySpace.SetQuantity(
                        ServiceLocator.GetService<IInventoryService>()
                            .TryPickItem(inventorySpace.ItemName, inventorySpace.Quantity));
                }
                Debug.Log($"Dropped {eventData.pointerDrag.name} into {gameObject.name}");
            }
        }
    }
}