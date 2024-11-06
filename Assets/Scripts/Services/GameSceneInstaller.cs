using System;
using Services.Interfaces;
using Services.Player;
using UnityEngine;

namespace Services
{
    public class GameSceneInstaller : MonoBehaviour
    {
        [SerializeField] private GameSceneReferences gameSceneReferences;
        [SerializeField] private InventoryService inventoryService;
        [SerializeField] private CraftingService craftingServie;
        [SerializeField] private TeleportService teleportService;
        private void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.Register<IGameSceneReferences>(gameSceneReferences);
            ServiceLocator.Register<IInventoryService>(inventoryService);
            ServiceLocator.Register<ICraftingService>(craftingServie);
            ServiceLocator.Register<ITeleportService>(teleportService);
        }
    }
}