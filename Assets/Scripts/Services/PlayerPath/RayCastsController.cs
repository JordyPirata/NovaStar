using System;
using UnityEngine;

namespace Services.PlayerPath
{
    public class RayCastsController
    {
        private IRayCastController _mediator;
        private Transform _playerTransform;
        
        public void Configure(IRayCastController playerMediator, Transform playerTransform)
        {
            _mediator = playerMediator;
            _playerTransform = playerTransform;
        }

        public void LookForGround()
        {
            if (Physics.Raycast(_playerTransform.position, Vector3.down, out var hit , 5500))
            {
                Debug.Log(hit.point);
                _playerTransform.position = hit.point;
            }
            else
            {
                Debug.Log("non hit");
            }
        }
    }
}