using System.Collections;
using UnityEngine;
using Services.Interfaces;
using System;

namespace Services.Player
{
    public class PlayerMediator : MonoBehaviour, IPlayerMediator
    {
        private IFirstPersonController _firstPersonCharacter;
        private IPlayerInfo _playerInfo;
        private IRayCastController _raycastController;
        private IHungerService _hungerService;
        private ILifeService _lifeService;
        private IStaminaService _staminaService;
        private IThirstService _hydrationService;
        private ITemperatureService _temperatureService;
        private IHUDService _hudService;

        private void Start()
        {
            _firstPersonCharacter = ServiceLocator.GetService<IFirstPersonController>();
            _raycastController = ServiceLocator.GetService<IRayCastController>();
            _lifeService = ServiceLocator.GetService<ILifeService>();
            _playerInfo = ServiceLocator.GetService<IPlayerInfo>();
            _staminaService = ServiceLocator.GetService<IStaminaService>();    
            _hydrationService = ServiceLocator.GetService<IThirstService>();
            _hungerService = ServiceLocator.GetService<IHungerService>();
            _temperatureService = ServiceLocator.GetService<ITemperatureService>();
            _hudService = ServiceLocator.GetService<IHUDService>();
            SubscribeToEvents();
        }
        // Actions
        Action HungerAction;
        Action ThirstAction;
        Action StaminaAction;
        Action LifeAction;
        Action TemperatureAction;
        Action TiredAction;
        private void SubscribeToEvents()
        {
            HungerAction = () => {_hudService.HungerValue = _hungerService.Hunger * 0.01f;};
            ThirstAction = () => {_hudService.ThirstValue = _hydrationService.Hydration * 0.01f;};
            StaminaAction = () => {_hudService.StaminaValue = _staminaService.Stamina * 0.01f;};
            LifeAction = () => {_hudService.HealthValue = _lifeService.Life * 0.01f;};
            TiredAction = () => 
            {
                Debug.Log("Tired changed");
                if (_staminaService.IsTired) ServiceLocator.GetService<IInputActions>().InputActions.Player.Run.Disable();
                else ServiceLocator.GetService<IInputActions>().InputActions.Player.Run.Enable();
            };
            Debug.Log("Subscribing to events");
            EventManager.OnMapLoaded += MapLoaded;

            _hungerService.OnStatChanged += HungerAction;
            _hydrationService.OnStatChanged += ThirstAction;
            _staminaService.OnStatChanged += StaminaAction;
            _lifeService.OnStatChanged += LifeAction;
            _staminaService.OnTiredChanged += TiredAction;
        }
        private void UnsubscribeToEvents()
        {
            EventManager.OnMapLoaded -= MapLoaded;
            _hungerService.OnStatChanged -= HungerAction;
            _hydrationService.OnStatChanged -= ThirstAction;
            _staminaService.OnStatChanged -= StaminaAction;
            _lifeService.OnStatChanged -= LifeAction;
            _staminaService.OnTiredChanged -= TiredAction;
        }
        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
        public void MapLoaded()
        {
            StartCoroutine(ExcecuteAfterMapLoaded());
        }
        
        public void PlayerDied()
        {
            _playerInfo.PlayerDied();
        }
        public void Run()
        {
            StartCoroutine(LoopDecreaseStamina());
        }
        public void StopRunning()
        {
            StopCoroutine(LoopDecreaseStamina());
        }
        private IEnumerator LoopDecreaseStamina()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                _staminaService.DecreaseStat(1);
            }
        }
        private IEnumerator ExcecuteAfterMapLoaded()
        {
            yield return new WaitForSeconds(4);
            _raycastController.LookForGround(_firstPersonCharacter.PlayerTransform);
            _firstPersonCharacter.ControllerScript.HasGravity = true;
            ServiceLocator.GetService<IFadeController>().FadeOut();
        }
    }
}