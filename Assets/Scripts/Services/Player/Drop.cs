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
        private List<int> droppedItemsIndex;
        private DropUIWindow _dropUIWindow;
        private List<string> _droppedItems;

        public override void Interact()
        {
            if (_droppedItems != null)
            {
                _dropUIWindow.OpenDrop(_droppedItems, this);
                return;
            }
            _dropUIWindow.OpenDrop(droppedItemsIndex, this);
        }

        public void Configure(int[] dropItemsData, DropUIWindow dropUIWindow)
        {
            droppedItemsIndex = dropItemsData.ToList();
            _dropUIWindow = dropUIWindow;
        }

        public void SetRestingItems(List<string> restingItems)
        {
            _droppedItems = restingItems;
        }
    }
}