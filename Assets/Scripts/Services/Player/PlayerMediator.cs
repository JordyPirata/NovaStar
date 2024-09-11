using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Services.Interfaces;
using System.IO.Compression;

namespace Services.Player
{
    public class PlayerMediator : MonoBehaviour, IPlayerMediator
    {
        private IPlayerInfo _playerInfo;
        private IRayCastController _raycastController;
        private IHungerService _hungerService;
        private ILifeService _lifeService;
        private IStaminaService _staminaService;
        private IHydrationService _hydrationService;
        private ITemperatureService _temperatureService;

        private void Awake()
        {
            _raycastController = ServiceLocator.GetService<IRayCastController>();
            _lifeService = ServiceLocator.GetService<ILifeService>();
            _playerInfo = ServiceLocator.GetService<IPlayerInfo>();
            _staminaService = ServiceLocator.GetService<IStaminaService>();    
            _hydrationService = ServiceLocator.GetService<IHydrationService>();
            _hungerService = ServiceLocator.GetService<IHungerService>();
            _temperatureService = ServiceLocator.GetService<ITemperatureService>();
        }
        private void Start()
        {
            _raycastController.Configure(this, _playerInfo.PlayerTransform());
        }
        public void Notify(object sender, string eventMessage)
        {
            /*
            switch (eventMessage)
            {
            // case "PlayerDied":
            }*/
        }
        public void PlayerDied()
        {
            _playerInfo.PlayerDied();
        }

        public void MapLoaded()
        {
            _raycastController.LookForGround();
        }
    }
    
}