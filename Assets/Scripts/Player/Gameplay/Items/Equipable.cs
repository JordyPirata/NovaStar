using UnityEngine;

namespace Player.Gameplay.Items
{
    public abstract class Equipable : MonoBehaviour
    {
        [ItemSelectorID] public int CorrespondentItem;

        public virtual void Equip()
        {
            
        }

        public virtual void UnEquip()
        {
            
        }
    }
}