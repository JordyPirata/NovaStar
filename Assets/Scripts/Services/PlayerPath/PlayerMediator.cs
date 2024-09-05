using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Services.PlayerPath
{
    public class PlayerMediator : MonoBehaviour , IRayCastController
    {
        private RayCastsController _raycastController;

        private void Awake()
        {
            _raycastController = new RayCastsController();
            _raycastController.Configure(this, transform);
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