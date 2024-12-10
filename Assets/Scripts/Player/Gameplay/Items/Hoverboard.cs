using Services.Interfaces;

namespace Player.Gameplay.Items
{
    public class Hoverboard : Equipable
    {
        public override void Equip()
        {
            base.Equip();
            ServiceLocator.GetService<IEquipablesService>().EquipHoverboard(true);
        }

        public override void UnEquip()
        {
            base.UnEquip();
            ServiceLocator.GetService<IEquipablesService>().EquipHoverboard(false);
        }
    }
}