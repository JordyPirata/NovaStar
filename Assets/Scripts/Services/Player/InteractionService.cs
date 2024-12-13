using Gameplay.Items;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Services.Player
{
    public class InteractionService : MonoBehaviour, IInteractionService
    {
        private IInputActions _inputActions;
        [SerializeField] private float _interactDistance;
        [SerializeField] private LayerMask _layerMask;
        private Camera _camera;
        private bool _canGetItems;

        public void Awake()
        {
            _inputActions = ServiceLocator.GetService<IInputActions>();
        }
        public void OnEnable()
        {
            _inputActions.InputActions.Player.Interact.started += InteractOnStarted;
        }
        public void OnDisable()
        {
            _inputActions.InputActions.Player.Interact.started -= InteractOnStarted;
        }
        public void SetCamera(Camera mainCamera)
        {
            _camera = mainCamera;
        }

        private void InteractOnStarted(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.started) return;
            ServiceLocator.GetService<ITeleportService>().Interacted();
            try
            {
                Interact();
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
            
        }
        private void Interact()
        {
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