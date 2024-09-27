using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Services.Interfaces;
using System.IO.Compression;
using Unity.Mathematics;
using System.Threading.Tasks;

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

        private void SubscribeToEvents()
        {
            Debug.Log("Subscribing to events");
            EventManager.OnMapLoaded += MapLoaded;
            _hungerService.OnStatChanged += () => {_hudService.HungerValue = _hungerService.Hunger * 0.01f;};
            _hydrationService.OnStatChanged += () => {_hudService.ThirstValue = _hydrationService.Hydration * 0.01f;};
            _staminaService.OnStatChanged += () => {_hudService.StaminaValue = _staminaService.Stamina * 0.01f;};
            _lifeService.OnStatChanged += () => {_hudService.HealthValue = _lifeService.Life * 0.01f;};
            
        }
        /*
        private void UnsubscribeToEvents()
        {
            EventManager.OnMapLoaded -= MapLoaded;
            _hungerService.OnStatChanged -= () => {_hudService.HungerValue = _hungerService.Hunger * 0.01f;};
            _hydrationService.OnStatChanged -= () => {_hudService.ThirstValue = _hydrationService.Hydration * 0.01f;};
            _staminaService.OnStatChanged -= () => {_hudService.StaminaValue = _staminaService.Stamina * 0.01f;};
            _lifeService.OnStatChanged -= () => {_hudService.HealthValue = _lifeService.Life * 0.01f;};
        }*/
        public void Notify(object sender, Event eventMessage)
        {
            /*
            switch (eventMessage)
            {
            // case "PlayerDied":
            }*/
        }
        
        public void MapLoaded()
        {
            StartCoroutine(ExcecuteAfterMapLoaded());
        }
        /*
        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }*/
        public void PlayerDied()
        {
            _playerInfo.PlayerDied();
        }
        public void StaminaEmpty()
        {
            ServiceLocator.GetService<IInputActions>().InputActions.Player.Run.Disable();
        }
        private IEnumerator ExcecuteAfterMapLoaded()
        {
            yield return new WaitForSeconds(4);
            _raycastController.LookForGround(_playerInfo.PlayerTransform());
            ServiceLocator.GetService<IFadeController>().FadeOut();
        }
    }
}