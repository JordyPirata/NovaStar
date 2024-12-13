using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class InventoryModel
    {
        public Dictionary<string, int> completeInventory;
        public List<InventorySpaceModel> fullInventory;
    }
}