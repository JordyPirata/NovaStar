using System;
using Services.Interfaces;
using UnityEngine;

namespace Services
{
    [Obsolete]
    public class GameSceneReferences : MonoBehaviour, IGameSceneReferences
    {
        [SerializeField] private Camera mainCamera;
        

        private void Start()
        {
            ServiceLocator.GetService<IInteractionService>().SetCamera(mainCamera);
        }
    }
}