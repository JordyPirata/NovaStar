using Services.Interfaces;
using UnityEngine;

namespace Services.Player
{
    public class HoverBoardService : MonoBehaviour, IHoverboardService
    {
        [SerializeField] private float hoverBoardSpeedMultiplier = 2;
        
        private bool _hoverboardEquipped;
        public bool HoverboardEquipped => _hoverboardEquipped;

        public float HoverBoardSpeedMultiplier => hoverBoardSpeedMultiplier;

        public void EquipHoverboard(bool b)
        {
            _hoverboardEquipped = b;
        }
    }
}