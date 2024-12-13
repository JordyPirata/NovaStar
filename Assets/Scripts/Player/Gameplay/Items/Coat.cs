using System.Collections;
using Services.Interfaces;

namespace Player.Gameplay.Items
{
    public class Coat : Equipable
    {
        public override void Equip()
        {
            base.Equip();
            ServiceLocator.GetService<IEquipablesService>().EquipCoat(true);
        }

        public override void UnEquip()
        {
            base.UnEquip();
            ServiceLocator.GetService<IEquipablesService>().EquipCoat(false);
        }
    }
}