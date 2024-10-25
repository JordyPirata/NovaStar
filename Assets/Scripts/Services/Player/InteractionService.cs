using System;
using Gameplay.Items;
using InputSystem;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Services.Player
{
    public class InteractionService : MonoBehaviour, IInteractionService
    {
        private IInputActions _inputActions;
        private float _interactDistance;
        private LayerMask _layerMask;
        private Camera _camera;

        public void Configure(float interactionDistance, LayerMask interactionLayer)
        {
            _inputActions = ServiceLocator.GetService<IInputActions>();
            _inputActions.InputActions.Player.Interact.started += InteractOnStarted;
            _interactDistance = interactionDistance;
            _layerMask = interactionLayer;
        }

        public void SetCamera(Camera mainCamera)
        {
            _camera = mainCamera;
        }

        private void InteractOnStarted(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.started) return;
            if (Physics.Raycast(_camera.transform.position,
                _camera.transform.TransformDirection(Vector3.forward), out var hit, _interactDistance, _layerMask))
            {
                if (hit.collider.TryGetComponent(out InteractableObject interactableObject))
                {
                    interactableObject.Interact();
                }
            }
        }
    }
}