using System;
using Cinemachine;
using InputSystem;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.Installer
{
    public class PlayerCameraService : MonoBehaviour, IPlayerCameraService
    {
        [SerializeField] private CinemachineBrain brain;
        [SerializeField] private CinemachineVirtualCamera thirdPersonVirtualCamera, firstPersonVirtualCamera;
        [SerializeField] private Renderer playerMesh;
        private InputActions _inputActions;
        private bool _thirdPersonCamera;

        private void Awake()
        {
            _inputActions = ServiceLocator.GetService<IInputActions>().InputActions;
            ChangeCamera(false);
        }

        private void ChangeCamera(bool thirdPerson)
        {
            firstPersonVirtualCamera.Priority = thirdPerson ? 0 : 10;
            thirdPersonVirtualCamera.Priority = thirdPerson ? 10 : 0;
            playerMesh.enabled = thirdPerson;
        }


        private void OnEnable()
        {
            _inputActions.Player.ChangeCamera.performed += OnChangeCamera;
        }

        private void OnDisable()
        {
            _inputActions.Player.ChangeCamera.performed -= OnChangeCamera;
        }
        
        private void OnChangeCamera(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _thirdPersonCamera = !_thirdPersonCamera;
            }

            ChangeCamera(_thirdPersonCamera);
            ServiceLocator.GetService<IPlayerMediator>().ChangeController(_thirdPersonCamera);
        }
        
    }
}