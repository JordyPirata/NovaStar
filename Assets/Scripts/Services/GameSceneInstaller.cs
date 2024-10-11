using System;
using Services.Interfaces;
using Services.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Services
{
    public class GameSceneInstaller : MonoBehaviour
    {
        [SerializeField] private GameSceneReferences gameSceneReferences;
        [SerializeField] private InventoryService inventoryService;
        private void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.Register<IGameSceneReferences>(gameSceneReferences);
            ServiceLocator.Register<IInventoryService>(inventoryService);
        }
    }
}