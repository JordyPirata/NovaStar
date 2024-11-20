using System;
using Services.Interfaces;

namespace Services.Player
{
    public class CollectionTool : Equipable
    {
        public override void Equip()
        {
            base.Equip();
            ServiceLocator.GetService<IInventoryService>().CanGetItems(true);
        }

        public override void UnEquip()
        {
            base.UnEquip();
            ServiceLocator.GetService<IInventoryService>().CanGetItems(false);
        }
    }
}