using UnityEngine;

namespace Services.Player
{
    public class Lantern : Equipable
    {
        [SerializeField] private Light lightComponent;
        public override void Equip()
        {
            base.Equip();
            lightComponent.enabled = true;
        }

        public override void UnEquip()
        {
            base.UnEquip();
            lightComponent.enabled = true;
        }
    }
}