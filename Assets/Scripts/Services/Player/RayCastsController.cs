using System;
using UnityEngine;
using Services.Interfaces;
using UnityEngine.SceneManagement;
using Services.WorldGenerator;

namespace Services.Player
{
    public class RayCastsController: IRayCastController
    {
        private Transform _playerTransform;
        /// <summary>
        /// Initializes the RayCastsController, with GameObject.Find("Player").transform
        /// </summary>
        public void Initialize()
        {
            _playerTransform = GameObject.Find("Player").transform;   
        }

        public void LookForGround()
        {
            if (_playerTransform == null)
            {
                throw new Exception("RayCastsController: Player Transform not found, try calling Initialize() first");
            }
            var height = ServiceLocator.GetService<IMap<ChunkObject>>()[0,0].TerrainData.GetHeight((int)_playerTransform.position.x, (int)_playerTransform.position.z);
            Debug.Log(height);
            if (Physics.Raycast(_playerTransform.position, Vector3.down, out var hit , 5500))
            {
                Debug.Log(hit.point);
                hit.point = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                _playerTransform.position = hit.point;
            }
            else
            {
                Debug.Log("non hit");
            }
        }
    }
}