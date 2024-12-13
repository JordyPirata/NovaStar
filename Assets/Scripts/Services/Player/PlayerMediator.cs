using System.Collections;
using UnityEngine;
using Services.Interfaces;
using System;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using Util;

namespace Services.Player
{
    public class PlayerMediator : MonoBehaviour, IPlayerMediator
    {
        private IInputActions _iInputActions;
        private IFirstPersonController _firstPersonCharacter;
        private IPlayerInfo _playerInfo;
        private IRayCastController _raycastController;
        private IHungerService _hungerService;
        private ILifeService _lifeService;
        private IStaminaService _staminaService;
        private IThirstService _hydrationService;
        private ITemperatureService _temperatureService;
        private IHUDService _hudService;
        private IInteractionService _interactionService;

        public bool IsTired => _staminaService.IsTired;
        public void Dehydrate(int i)
        {
            _hydrationService.DecreaseStat(1);
        }

        public void LimitStamina(int i)
        {
            _staminaService.SetMaxStamina(i);
        }

        public void LoseLife(int i)
        {
            _lifeService.DecreaseStat(i);
        }

        public void EquipCoat(bool b)
        {
            _temperatureService.EquipCoat(b);
        }

        public void StopLifeRegen(bool b)
        {
            var service = _lifeService as IService;

            if (b)
            {
                service?.StartService();    
            }
            else
            {
                service?.StopService();
            }
        }

        public void Start()
        {
            _iInputActions = ServiceLocator.GetService<IInputActions>();
            _firstPersonCharacter = ServiceLocator.GetService<IFirstPersonController>();
            _raycastController = ServiceLocator.GetService<IRayCastController>();
            _lifeService = ServiceLocator.GetService<ILifeService>();
            _playerInfo = ServiceLocator.GetService<IPlayerInfo>();
            _staminaService = ServiceLocator.GetService<IStaminaService>();    
            _hydrationService = ServiceLocator.GetService<IThirstService>();
            _hungerService = ServiceLocator.GetService<IHungerService>();
            _temperatureService = ServiceLocator.GetService<ITemperatureService>().Configure(this);
            _hudService = ServiceLocator.GetService<IHUDService>();
            SubscribeToEvents();
        }
        // Actions
        Action HungerAction;
        Action ThirstAction;
        Action StaminaAction;
        Action LifeAction;
        Action TemperatureAction;
        Action<bool> TiredAction;

        private void SubscribeToEvents()
        {
            
            HungerAction = () => {_hudService.HungerValue = _hungerService.Hunger * 0.01f;};
            ThirstAction = () => {_hudService.ThirstValue = _hydrationService.Hydration * 0.01f;};
            StaminaAction = () => {_hudService.StaminaValue = _staminaService.Stamina * 0.01f;};
            LifeAction = () => {_hudService.HealthValue = _lifeService.Life * 0.01f;};
            TiredAction = (bool b ) => 
            {
                if (b)
                {
                    _iInputActions.InputActions.Player.Run.Disable();
                    
                }
                else _iInputActions.InputActions.Player.Run.Enable();
            };
            Debug.Log("Subscribing to events");

            _iInputActions.InputActions.Player.Run.performed += OnPlayerRunning;
            _iInputActions.InputActions.Player.Run.canceled += OnPlayerRunning;

            EventManager.OnMapLoaded += MapLoaded;

            _hungerService.OnStatChanged += HungerAction;
            _hydrationService.OnStatChanged += ThirstAction;
            _staminaService.OnStatChanged += StaminaAction;
            _lifeService.OnStatChanged += LifeAction;
            _staminaService.OnTiredChanged += TiredAction;
        }
        private void UnsubscribeToEvents()
        {
            _iInputActions.InputActions.Player.Run.performed -= OnPlayerRunning;
            _iInputActions.InputActions.Player.Run.canceled -= OnPlayerRunning;

            EventManager.OnMapLoaded -= MapLoaded;

            _hungerService.OnStatChanged -= HungerAction;
            _hydrationService.OnStatChanged -= ThirstAction;
            _staminaService.OnStatChanged -= StaminaAction;
            _lifeService.OnStatChanged -= LifeAction;
            _staminaService.OnTiredChanged -= TiredAction;
        }
        public void OnDestroy()
        {
            UnsubscribeToEvents();
        }
        public void MapLoaded()
        {
            StartCoroutine(ExcecuteAfterMapLoaded());
        }

        public void TeleportToPosition(float3 dataTeleportPosition)
        {
            ServiceLocator.GetService<IFadeController>().FadeIn(()=>
            {
                _firstPersonCharacter.TeleportToPosition(dataTeleportPosition); 
            });
        }

        public bool UseConsumable(ConsumableType consumableType)
        {
            switch (consumableType)
            {
                case ConsumableType.Hat:
                    _temperatureService.EquipHat();
                    return true;
                case ConsumableType.WaterBottle:
                    var drunk = _hydrationService.DrinkSomeWater();
                    if (drunk) _temperatureService.DrinkSomeWater();
                    return drunk;
                case ConsumableType.CampFire:
                    return false;
                case ConsumableType.Food:
                    return _hungerService.EatSomeFood();
                case ConsumableType.Stimulant:
                    _staminaService.Stimulate();
                    _firstPersonCharacter.Stimulate();
                    _lifeService.Stimulate();
                    return true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(consumableType), consumableType, null);
            }
        }


        public void PlayerDied()
        {
            _playerInfo.PlayerDied();
        }
        public void OnPlayerRunning(InputAction.CallbackContext context)
        {
            if (context.performed) _staminaService.Increase = false;
            if (context.canceled) _staminaService.Increase = true;
        }
        private IEnumerator ExcecuteAfterMapLoaded()
        {
            yield return new WaitForSeconds(4);
            _raycastController.LookForGround(_firstPersonCharacter.PlayerTransform);
            yield return new WaitForSeconds(0.1f);
            _firstPersonCharacter.CanMove = true;
            _iInputActions.InputActions.Player.Enable();
            _playerInfo.MapLoaded();
            _temperatureService.MapLoaded();
            
            ServiceLocator.GetService<IFadeController>().FadeOut();
            ServiceLocator.GetService<ITimeService>().StartRunningTime();
        }
    }
}