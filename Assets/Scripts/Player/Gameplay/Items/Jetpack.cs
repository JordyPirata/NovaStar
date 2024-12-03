using Services.Interfaces;

namespace Player.Gameplay.Items
{
    public class Jetpack : Equipable
    {
        public override void Equip()
        {
            base.Equip();
            ServiceLocator.GetService<IEquipablesService>().EquipJetpack(false, true);
        }

        public override void UnEquip()
        {
            base.UnEquip();
            ServiceLocator.GetService<IEquipablesService>().EquipJetpack(false, false);
        }
    }
}