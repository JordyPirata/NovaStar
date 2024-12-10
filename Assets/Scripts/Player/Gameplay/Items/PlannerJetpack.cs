using Services.Interfaces;

namespace Player.Gameplay.Items
{
    public class PlannerJetpack : Equipable
    {
        public override void Equip()
        {
            base.Equip();
            ServiceLocator.GetService<IEquipablesService>().EquipJetpack(true, true);
            ServiceLocator.GetService<IEquipablesService>().EquipPlanner(true);
        }

        public override void UnEquip()
        {
            base.UnEquip();
            ServiceLocator.GetService<IEquipablesService>().EquipJetpack(true, false);
            ServiceLocator.GetService<IEquipablesService>().EquipPlanner(false);
        }
    }
}