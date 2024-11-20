using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services.Player
{
    public class Drop : InteractableObject
    {
        [SerializeField] private List<int> droppedItemsIndex;
        [SerializeField] private MovingInventorySpace movingInventorySpace;
        private DropUIWindow _dropUIWindow;

        public override void Interact()
        {
            _dropUIWindow.OpenDrop(droppedItemsIndex);
        }

        public void Configure(int[] dropItemsData, DropUIWindow dropUIWindow)
        {
            droppedItemsIndex = dropItemsData.ToList();
            _dropUIWindow = dropUIWindow;
        }
    }
}