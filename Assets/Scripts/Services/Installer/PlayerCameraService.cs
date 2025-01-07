using System;
using Cinemachine;
using UnityEngine;

namespace Services.Installer
{
    public class PlayerCameraService : MonoBehaviour, IPlayerCameraService
    {
        [SerializeField] private CinemachineBrain brain;
        [SerializeField] private CinemachineVirtualCamera thirdPersonVirtualCamera, firstPersonVirtualCamera;

        private void Start()
        {
            firstPersonVirtualCamera.Priority = 10;
            thirdPersonVirtualCamera.Priority = 0;
        }
        
        
    }
}