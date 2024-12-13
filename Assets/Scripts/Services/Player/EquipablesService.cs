using Services.Interfaces;
using UnityEngine;

namespace Services.Player
{
    public class EquipablesService : IEquipablesService
    {
        private int _plannersEquipped, _jetpacksEquipped, _infiniteJetpacksEquipped, _hoverboardsEquipped, _coatsEquipped;
        public void EquipPlanner(bool b)
        {
            _plannersEquipped = b ? _plannersEquipped + 1 : _plannersEquipped - 1;
            CanPlane(_plannersEquipped > 0); 
        }

        public void EquipJetpack(bool isInfinite, bool equip)
        {
            if (equip)
            {
                _jetpacksEquipped++;
                if (isInfinite) _infiniteJetpacksEquipped++;
            }
            else
            {
                _jetpacksEquipped--;
                if (isInfinite) _infiniteJetpacksEquipped--;
            }

            ServiceLocator.GetService<IJetPackService>()
                .EquipJetpack(_jetpacksEquipped > 0, _infiniteJetpacksEquipped > 0);
        }

        public void EquipHoverboard(bool b)
        {
            _plannersEquipped = b ? _plannersEquipped + 1 : _plannersEquipped - 1;
            ServiceLocator.GetService<IHoverboardService>().EquipHoverboard(b);
        }

        public void EquipCoat(bool b)
        {
            _coatsEquipped = b ? _coatsEquipped + 1 : _coatsEquipped - 1;
            ServiceLocator.GetService<IPlayerMediator>().EquipCoat(_coatsEquipped > 0);
        }

        private void CanPlane(bool b)
        {
            ServiceLocator.GetService<IFirstPersonController>().CanPlane = b;
        }
    }
}