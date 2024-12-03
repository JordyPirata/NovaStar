using System;
using System.Collections;
using InputSystem;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.Player
{
    public class JetPackService : MonoBehaviour, IJetPackService
    {
        [SerializeField] private float maxSecondsFuel = 5, propellingForce = 5, startPropellingTime = .6f, startRecoveringFuelTime = 2;
        private InputActions _inputActions;
        private bool _propelling, _waitingToRecover;
        private bool _isEquipped, _isInfinite;
        private float _currentFuel;

        public bool Propelling => _propelling;
        public float PropellingForce => propellingForce;


        private void Awake()
        {
            _currentFuel = maxSecondsFuel;
            _inputActions = ServiceLocator.GetService<IInputActions>().InputActions;
        }
        
        private void OnEnable()
        {
            _inputActions.Player.Jump.started += OnJump;
            _inputActions.Player.Jump.canceled += OnJump;
        }

        private void OnDisable()
        {
            _inputActions.Player.Jump.started -= OnJump;
            _inputActions.Player.Jump.canceled -= OnJump;
        }
        
        public void EquipJetpack(bool equip, bool isInfinite)
        {
            _isEquipped = equip;
            _isInfinite = isInfinite;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (!_isEquipped) return;
            if (context.started)
            {
                _waitingToRecover = false;
                StopAllCoroutines();
                StartCoroutine(StartPropelling());
            }
            else if (context.canceled)
            {
                StopAllCoroutines();
                if (!_waitingToRecover) StartCoroutine(StartRecoveringFuel());
                _propelling = false;
            }
        }

        private IEnumerator StartPropelling()
        {/*
            yield return new WaitForSeconds(startPropellingTime);*/
            _propelling = true;
            if (!_isInfinite)
            {
                Debug.Log("Propelling");
                while (_currentFuel > 0)
                {
                    yield return new WaitForSeconds(.04f);
                    _currentFuel -= .04f;
                    Debug.Log(_currentFuel);
                }
                _propelling = false;
                if (!_waitingToRecover) StartCoroutine(StartRecoveringFuel());
            } 
        }

        private IEnumerator StartRecoveringFuel()
        {
            if (!_isInfinite)
            {
                _waitingToRecover = true;
                yield return new WaitForSeconds(startRecoveringFuelTime);
                Debug.Log("Recovering");
                while (_currentFuel < maxSecondsFuel)
                {
                    yield return new WaitForSeconds(.04f);
                    _currentFuel += .04f;
                    Debug.Log(_currentFuel);
                }
                _waitingToRecover = false;
            }
        }
    }
}