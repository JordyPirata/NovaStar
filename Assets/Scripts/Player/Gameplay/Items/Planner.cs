using Services.Installer;
using Services.Interfaces;

namespace Player.Gameplay.Items
{
    public class Planner : Equipable
    {
        public override void Equip()
        {
            base.Equip();
            ServiceLocator.GetService<IEquipablesService>().EquipPlanner(true);
        }

        public override void UnEquip()
        {
            base.UnEquip();
            ServiceLocator.GetService<IEquipablesService>().EquipPlanner(false);
        }
    }
}