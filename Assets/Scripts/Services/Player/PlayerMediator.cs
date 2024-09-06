using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Services.Interfaces;

namespace Services.Player
{
    public class PlayerMediator : MonoBehaviour, IPlayerMediator
    {
        public IRayCastController _raycastController { get; set; }
        public IPlayerMediator _playerMediator { get; set; }
        public ILifeService _lifeService { get; set; }
        public IPlayerInfo _playerInfo { get; set; }
        public IStaminaService _staminaService { get; set; }

        private void Start()
        {
            _playerMediator = ServiceLocator.GetService<IPlayerMediator>();
            _raycastController = ServiceLocator.GetService<IRayCastController>();
            _lifeService = ServiceLocator.GetService<ILifeService>();
            _playerInfo = ServiceLocator.GetService<IPlayerInfo>();
            _staminaService = ServiceLocator.GetService<IStaminaService>();
    
            _raycastController.Configure(_playerMediator, transform);
        }

        public void MapLoaded()
        {
            _raycastController.LookForGround();
        }
    }
    
}