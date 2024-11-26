using System;
using System.Collections.Generic;
using System.Linq;
using InputSystem;
using Player.Gameplay.UserInterface;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services
{
    public class UIService : MonoBehaviour, IUIService
    {
        [Serializable]
        public class UIPanel
        {
            public UIPanelType panelType;
            public GameObject panelGameObject;
        }

        [SerializeField] private List<UIPanel> panels;

        private InputActions _inputActions;
        private UIPanelType _openedPanelType;

        private void Awake()
        {
            _inputActions = ServiceLocator.GetService<IInputActions>().InputActions;
            _inputActions.Player.InventoryMenu.performed += OnInventoryMenu;
            foreach (var panel in panels)
            {
                panel.panelGameObject.SetActive(false);
            }
        }

        private void OnInventoryMenu(InputAction.CallbackContext obj)
        {
            OpenUIPanel(UIPanelType.Inventory);
        }

        public bool OpenUIPanel(UIPanelType uiPanelType)
        {
            if (_openedPanelType != UIPanelType.None)
            {
                if (_openedPanelType != uiPanelType) return false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                foreach (var panel in panels.Where(panel => panel.panelType == uiPanelType))
                {
                    panel.panelGameObject.SetActive(false);
                    _openedPanelType = UIPanelType.None;
                    return true;
                }    
            }
            foreach (var panel in panels.Where(panel => panel.panelType == uiPanelType))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                panel.panelGameObject.SetActive(true);
                _openedPanelType = uiPanelType;
                return true;
            }
            throw new Exception($"didnt found panel with the name {uiPanelType.ToString()}");
        }
    }
}