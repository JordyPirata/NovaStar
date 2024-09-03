using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem;
using Services;
using UnityEngine.InputSystem.Controls;
// TODO: Change to use Unity's Event System
[RequireComponent(typeof(CharacterController))]
public class FirstPersonCharacter : MonoBehaviour
{
    private InputActions inputActions;

    private CharacterController controller;
    
    [SerializeField] private Camera cam;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] public float lookSensitivity = 1.0f;
    
    private float xRotation = 0f;

    // Movement Vars
    private Vector3 velocity;
    public float gravity = -15.0f;
    private bool grounded;

    // Zoom Vars - Zoom code adapted from @torahhorse's First Person Drifter scripts.
    public float zoomFOV = 35.0f;
    public float zoomSpeed = 9f;
    private float targetFOV;
    private float baseFOV;

    // Crouch Vars
    private float initHeight;
    [SerializeField] private float crouchHeight;
    // Sprint Vars
    [SerializeField] private bool sprinting = false;
    private void Awake()
    {
        inputActions = ServiceLocator.GetService<IInputActions>().InputActions;
        //lookSensitivity = ServiceLocator.GetService<ISettingsService>().GetSensitibility();
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        initHeight = controller.height;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        SetBaseFOV(cam.fieldOfView);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += DoJump;
        inputActions.Player.Run.started += OnSprint;
        inputActions.Player.Run.canceled += OnSprint;
    }
    private void OnDisable()
    {
        inputActions.Player.Run.started += OnSprint;
        inputActions.Player.Run.canceled += OnSprint;
        inputActions.Player.Jump.performed -= DoJump;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        ApplyGravity();
        DoMovement();
        DoLooking();
        DoZoom();
        DoCrouch();
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
        controller.Move(velocity * Time.deltaTime);
    }
    private void OnSprint(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if(context.started) sprinting = true;
        if (context.canceled) sprinting = false;
    }
    private void DoMovement()
    {
        float targetSpeed = sprinting ? movementSpeed * 2 : movementSpeed;
        grounded = controller.isGrounded;
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        Vector2 movement = GetPlayerMovement();
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        controller.Move(targetSpeed * Time.deltaTime * move);
    }
    private void DoJump(InputAction.CallbackContext context)
    {
        if (context.performed && grounded)
        {
            velocity.y = Mathf.Sqrt(2.0f * -2.0f * gravity);
        }
    }

    private void DoZoom()
    {
        if (inputActions.Player.Zoom.ReadValue<float>() > 0)
        {
            targetFOV = zoomFOV;
        }
        else
        {
            targetFOV = baseFOV;
        }
        UpdateZoom();
    }

    private void DoCrouch()
    {
        if (inputActions.Player.Crouch.ReadValue<float>() > 0)
        {
            controller.height = crouchHeight;
        }
        else
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), 2.0f, -1))
            {
                controller.height = crouchHeight;
            }
            else
            {
                controller.height = initHeight;
            }
        }
    }

    private void UpdateZoom()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
    }

    public void SetBaseFOV(float fov)
    {
        baseFOV = fov;
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
