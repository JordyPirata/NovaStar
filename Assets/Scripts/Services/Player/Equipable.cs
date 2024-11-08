using Player.Gameplay;
using UnityEngine;

namespace Services.Player
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