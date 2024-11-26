using Services.Interfaces;
using Services.Player;

namespace Player.Gameplay.Items
{
    public class TeleportTablet : Equipable
    {
        public override void Equip()
        {
            base.Equip();
            ServiceLocator.GetService<ITeleportService>().EquipTeleport(true);
        }

        public override void UnEquip()
        {
            base.UnEquip();
            ServiceLocator.GetService<ITeleportService>().EquipTeleport(false);
        }
    }
}