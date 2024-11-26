using System.Collections.Generic;
using System.Linq;
using Player.Gameplay.UserInterface;
using Services.Interfaces;
using TMPro;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Services
{
    public class TeleportService : MonoBehaviour, ITeleportService
    {
        [SerializeField] private TMP_InputField tpNameInputField;
        [SerializeField] private TeleportUI teleportUITemplate;
        private int _teleportsEquipped;
        private bool _teleportMenuOpen;
        private bool CanOpenTeleportMenu => _teleportsEquipped > 0;
        private List<TeleportData> _teleportData;
        private Transform _telelportsUIParentTransform;

        private void Awake()
        {
            _teleportData = new List<TeleportData>();
            _telelportsUIParentTransform = teleportUITemplate.transform.parent;
            teleportUITemplate.gameObject.SetActive(false);
        }

        public void Interacted()
        {
            if (CanOpenTeleportMenu)
            {
                if (!_teleportMenuOpen)
                {
                    if (ServiceLocator.GetService<IUIService>().OpenUIPanel(UIPanelType.Teleport))
                        _teleportMenuOpen = !_teleportMenuOpen;
                }
            }
        }

        public void Close()
        {
            if (_teleportMenuOpen)
            {
                _teleportMenuOpen = !_teleportMenuOpen;
                ServiceLocator.GetService<IUIService>().OpenUIPanel(UIPanelType.Teleport);
            }
        }
        
        public void EquipTeleport(bool canOpen)
        {
            if (canOpen)
                _teleportsEquipped++;
            else
                _teleportsEquipped--;

            
            if (_teleportsEquipped < 0) Debug.LogError($"For some reason you have {_teleportsEquipped} teleports equipped now");
        }

        public void SetTeleportInPlayerPosition()
        {
            if (_teleportData.Any(data => data.teleportName == tpNameInputField.text))
                return;
            
            var teleportData = new TeleportData()
            {
                teleportPosition = ServiceLocator.GetService<IPlayerInfo>().PlayerPosition(),
                teleportName = tpNameInputField.text
            };
            _teleportData.Add(teleportData);
            var teleportUIInstance = Instantiate(teleportUITemplate, _telelportsUIParentTransform);
            teleportUIInstance.TeleportName = teleportData.teleportName;
            teleportUIInstance.OnClick += TeleportToPosition;
            teleportUIInstance.gameObject.SetActive(true);
        }

        private void TeleportToPosition(string teleportName)
        {
            foreach (var data in _teleportData.Where(data=> data.teleportName == teleportName))
            {
                ServiceLocator.GetService<IPlayerMediator>().TeleportToPosition(data.teleportPosition);
            }
        }
    }
}