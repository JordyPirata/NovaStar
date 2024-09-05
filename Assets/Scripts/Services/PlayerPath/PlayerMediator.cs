using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Services.Interfaces;

namespace Services.PlayerPath
{
    public class PlayerMediator : MonoBehaviour, IPlayerMediator
    {
        private IRayCastController _raycastController;
        private IPlayerMediator _playerMediator;

        private void Awake()
        {
            _playerMediator = ServiceLocator.GetService<IPlayerMediator>();
            _raycastController = ServiceLocator.GetService<IRayCastController>();
            _raycastController.Configure(_playerMediator, transform);
            
        }

        public void MapLoaded()
        {
            _raycastController.LookForGround();
        }

        private void Update()
        {
           // _raycastController.LookForGround();
        }
    }
    
}