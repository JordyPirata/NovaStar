using System;
using UnityEngine;
using Services.Interfaces;
using UnityEngine.SceneManagement;
using Services.WorldGenerator;

namespace Services.Player
{
    public class RayCastsController: IRayCastController
    {
        readonly IMap<ChunkObject> Map = ServiceLocator.GetService<IMap<ChunkObject>>();
        public void LookForGround( Transform playerTransform)
        {
            if (playerTransform == null)
            {
                throw new Exception("RayCastsController: Player Transform not found, try calling Initialize() first");
            }
            var height = Map[0,0].TerrainData.GetHeight((int)playerTransform.position.x, (int)playerTransform.position.z);
            Debug.Log(height);
            if (Physics.Raycast(playerTransform.position, Vector3.down, out var hit , 5500))
            {
                Debug.Log(hit.point);
                hit.point = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                playerTransform.position = hit.point;
            }
            else
            {
                Debug.Log("non hit");
            }
        }
        public void CheckFall(Transform playerTransform)
        {
            if (playerTransform == null)
            {
                throw new Exception("RayCastsController: Player Transform not found, try calling Initialize() first");
            }
            if (Physics.Raycast(playerTransform.position, Vector3.down, out var hit , 1000))
            {
                if (hit.distance > 2)
                {
                    Debug.Log("Falling");
                }
            }
        }
    }
}