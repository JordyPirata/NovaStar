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
        [SerializeField] private CraftingService craftingService;
        [SerializeField] private TeleportService teleportService;
        [SerializeField] private TimeService timeService;
        [SerializeField] private DropsService dropsService;
        [SerializeField] private UIService uiService;
        private void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.Register<IGameSceneReferences>(gameSceneReferences);
            ServiceLocator.Register<IInventoryService>(inventoryService);
            ServiceLocator.Register<ICraftingService>(craftingService);
            ServiceLocator.Register<ITeleportService>(teleportService);
            ServiceLocator.Register<ITimeService>(timeService);
            ServiceLocator.Register<IUIService>(uiService);
        }
    }
}