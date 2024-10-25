using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem;
using Services.Interfaces;
using Unity.VisualScripting;

namespace Services
{
[RequireComponent(typeof(CharacterController))]
public class FirstPersonCharacter : MonoBehaviour
{
    private InputActions inputActions;
    public CharacterController Controller { get; set; }
    public Transform PlayerTransform 
    {
        get => Controller.transform;
    }

    [SerializeField] private Camera cam;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private float lookSensitivity = 1.0f;
    
    private float xRotation = 0f;

    // Movement Vars
    private Vector3 velocity;
    public float gravity = -15.0f;
    private float initHeight;
    [SerializeField] private float crouchHeight;
    // Sprint Vars
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;

    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;
    private bool Sprinting;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.6f;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;
    public bool CanMove;

    private void Awake()
    {
        inputActions = ServiceLocator.GetService<IInputActions>().InputActions;
        lookSensitivity = ServiceLocator.GetService<ISettingsService>().GetSensitibility() * 10;
    }
    private void Start()
    {
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
        DoMovement();
        DoLooking();
        
    }

    private void DoLooking()
    {
        Vector2 looking = GetPlayerLook();
        float lookX = looking.x * lookSensitivity * Time.deltaTime;
        float lookY = looking.y * lookSensitivity * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * lookX);
    }
    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        Controller.Move(velocity * Time.deltaTime);
    }
    private void OnSprint(InputAction.CallbackContext context)
    {
        if(context.started) Sprinting = true;
        else if(context.canceled) Sprinting = false;
    }
    private void DoMovement()
    {
        float targetSpeed = Sprinting ? movementSpeed * 2 : movementSpeed;
        Grounded = Controller.isGrounded;
        if (Grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        Vector2 movement = GetPlayerMovement();
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        Controller.Move(targetSpeed * Time.deltaTime * move);
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

    public Vector2 GetPlayerMovement()
    {
        return inputActions.Player.Move.ReadValue<Vector2>(); 
    }

    public Vector2 GetPlayerLook()
    {
        return inputActions.Player.Look.ReadValue<Vector2>();
    }
}
}