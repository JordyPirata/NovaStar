using System;
using System.Collections.Generic;
using Gameplay.Items.Crafting;
using Services.Player;

namespace Services.Interfaces
{
    public interface IInventoryService
    {
        int TryPickItem(string item, int quantity, bool needsTool = false);
        ref Action<Dictionary<string, int>> GetOnInventoryUpdated();
        void TryCraftItem(CraftingRecipe recipe);
        public void CanGetItems(bool canGetItems);
    }
}