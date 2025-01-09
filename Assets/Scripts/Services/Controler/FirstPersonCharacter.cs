using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem;
using Services.Interfaces;
using Unity.Mathematics;

namespace Services
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonCharacter : MonoBehaviour, IFirstPersonController
    {
        private InputActions inputActions;
        public CharacterController Controller { get; set; }

        public Transform PlayerTransform
        {
            get => Controller.transform;
        }

        public bool CanPlane { get; set; }
        
        
        public bool Running => Sprinting && !ServiceLocator.GetService<IPlayerMediator>().IsTired;

        [SerializeField] private CinemachineVirtualCamera firstPersonCamera;
        [SerializeField] private GameObject thirdPersonCamera;
        [SerializeField] private Transform playerModelTransform;
        [SerializeField] private float movementSpeed = 2.0f, stimulatedSpeed = 3.0f;
        [SerializeField] private float lookSensitivity = 1.0f;

        private float xRotation = 0f;

        // Movement Vars
        private Vector3 velocity;
        public float gravity = -15.0f, maxPlanningVelocity = -2f;
        private float initHeight;
        private float _restingStimulatedTime;

        [SerializeField] private float crouchHeight;

        // Sprint Vars
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")] public float GroundedOffset = -0.14f;
        private bool Sprinting;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.6f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;
        
        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;
        

        public bool CanMove { get; set; }
        public bool Stimulated { get; set; }

        private void Awake()
        {
            inputActions = ServiceLocator.GetService<IInputActions>().InputActions;
            lookSensitivity = ServiceLocator.GetService<ISettingsService>().GetSensitibility() * 10;
        }

        private void Start()
        {
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            Controller = GetComponent<CharacterController>();
            initHeight = Controller.height;
            Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            // SetBaseFOV(cam.fieldOfView);
        }

        private void OnEnable()
        {
            inputActions.Player.Crouch.canceled += DoCrouch;
            inputActions.Player.Crouch.performed += DoCrouch;
            inputActions.Player.Jump.performed += DoJump;
            inputActions.Player.Run.started += OnSprint;
            inputActions.Player.Run.canceled += OnSprint;
        }

        private void OnDisable()
        {
            inputActions.Player.Crouch.canceled -= DoCrouch;
            inputActions.Player.Crouch.performed -= DoCrouch;
            inputActions.Player.Run.started -= OnSprint;
            inputActions.Player.Run.canceled -= OnSprint;
            inputActions.Player.Jump.performed -= DoJump;
        }

        void FixedUpdate()
        {
            if (!CanMove) return;

            ApplyGravity();
            GroundedCheck();
            
            CameraRotation();
            if (_thirdPerson)
            {
                Move();
            }
            else
            {
                DoMovement();
                DoLooking();
            }
        }


        #region ThirdPerson

        
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        private float _targetRotation = 0f;
        private const float _threshold = 0.01f;
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;
        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public float deltaTimeMultiplier = .1f;

        private float _speed;
        private float _rotationVelocity;
        private bool _thirdPerson;


        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (GetPlayerLook().sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;

                _cinemachineTargetYaw += GetPlayerLook().x * deltaTimeMultiplier;
                _cinemachineTargetPitch -= GetPlayerLook().y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            var targetSpeed = Stimulated ? stimulatedSpeed : movementSpeed;
            targetSpeed = Running ? targetSpeed * 2 : targetSpeed;
            targetSpeed = ServiceLocator.GetService<IHoverboardService>().HoverboardEquipped
                ? ServiceLocator.GetService<IHoverboardService>().HoverBoardSpeedMultiplier * targetSpeed
                : targetSpeed;
            
            if (GetPlayerMovement() == Vector2.zero) targetSpeed = 0.0f;
            if (GetPlayerMovement() != Vector2.zero)
            {
                Vector3 inputDirection = new Vector3(GetPlayerMovement().x, 0.0f, GetPlayerMovement().y).normalized;

                // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
                // if there is a move input rotate player when the player is moving
                if (GetPlayerMovement() != Vector2.zero)
                {
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                      thirdPersonCamera.transform.eulerAngles.y;
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);

                    // rotate to face input direction relative to camera position
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }


                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

                // move the player
                Controller.Move(targetDirection.normalized * (targetSpeed * Time.deltaTime));
            }
            ServiceLocator.GetService<IPlayerAnimator>().PlayerWalking(targetSpeed / 10);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
        #endregion
        
        
        private void DoLooking()
        {
            Vector2 looking = GetPlayerLook();
            float lookX = looking.x * lookSensitivity * Time.deltaTime;
            float lookY = looking.y * lookSensitivity * Time.deltaTime;

            xRotation -= lookY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            firstPersonCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            transform.Rotate(Vector3.up * lookX);
        }

        private void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            if (ServiceLocator.GetService<IJetPackService>().Propelling)
                velocity.y = ServiceLocator.GetService<IJetPackService>().PropellingForce;
            var planning = CanPlane && velocity.y < maxPlanningVelocity;
            if (planning) velocity.y = maxPlanningVelocity;
            ServiceLocator.GetService<IPlayerAnimator>().PlayerGliding(planning);
            Controller.Move(velocity * Time.deltaTime);
        }

        private void OnSprint(InputAction.CallbackContext context)
        {
            if (context.started) Sprinting = true;
            else if (context.canceled) Sprinting = false;
        }
        

        private void DoMovement()
        {
            var targetSpeed = Stimulated ? stimulatedSpeed : movementSpeed;
            targetSpeed = Running ? targetSpeed * 2 : targetSpeed;
            targetSpeed = ServiceLocator.GetService<IHoverboardService>().HoverboardEquipped
                ? ServiceLocator.GetService<IHoverboardService>().HoverBoardSpeedMultiplier * targetSpeed
                : targetSpeed;
            Grounded = Controller.isGrounded;
            if (Grounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            Vector2 movement = GetPlayerMovement();
            Vector3 move = transform.right * movement.x + transform.forward * movement.y;
            /*playerModelTransform.LookAt(playerModelTransform.position + move);*/
            ServiceLocator.GetService<IPlayerAnimator>().PlayerWalking(targetSpeed);
            Controller.Move(targetSpeed * Time.deltaTime * move);
            
            if (Stimulated)
            {
                _restingStimulatedTime -= Time.fixedTime;
                if (_restingStimulatedTime <= 0) Stimulated = false;
            }
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);
        }

        private void DoJump(InputAction.CallbackContext context)
        {
            if (context.performed && Grounded)
            {
                velocity.y = Mathf.Sqrt(2.0f * -2.0f * gravity);
                ServiceLocator.GetService<IPlayerAnimator>().PlayerJump();
            }
        }

        private void DoCrouch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Controller.height = crouchHeight;
            }

            if (context.canceled)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), 2.0f, -1))
                {
                    Controller.height = crouchHeight;
                }
                else
                {
                    Controller.height = initHeight;
                }
            }
        }

        public void Stimulate()
        {
            Stimulated = true;
            _restingStimulatedTime = 60f;
        }

        public void ChangeControllerType(bool thirdPerson)
        {
            _thirdPerson = thirdPerson;
        }

        public Vector2 GetPlayerMovement()
        {
            return inputActions.Player.Move.ReadValue<Vector2>();
        }

        public Vector2 GetPlayerLook()
        {
            return inputActions.Player.Look.ReadValue<Vector2>();
        }

        public void TeleportToPosition(float3 dataTeleportPosition)
        {
            CanMove = false;
            PlayerTransform.position = dataTeleportPosition;
            StartCoroutine(WaitToTeleport());
        }
        
        private IEnumerator WaitToTeleport()
        {
            yield return null;
            yield return new WaitForFixedUpdate();
            CanMove = true;
            ServiceLocator.GetService<IFadeController>().FadeOut();
        }
    }
}