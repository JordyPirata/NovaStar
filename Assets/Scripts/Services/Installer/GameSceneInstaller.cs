using System;
using Services.Interfaces;
using Services.Player;
using UnityEngine;
using Services.Splatmap;
using Services.WorldGenerator;
namespace Services
{
[RequireComponent(typeof(PlayerMediatorData))]
public class GameSceneInstaller : MonoBehaviour
{
    [SerializeField] private PlayerMediatorData playerMediatorData;
    [SerializeField] private GameSceneReferences gameSceneReferences;
    [SerializeField] private InventoryService inventoryService;
    [SerializeField] private CraftingService craftingService;
    [SerializeField] private TeleportService teleportService;
    [SerializeField] private TimeService timeService;
    [SerializeField] private DropsService dropsService;
    [SerializeField] private UIService uiService;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private InteractionService interactionService;
    [SerializeField] private BiomeTexturesService biomeTexturesService;
    private void Awake()
    {
        RegisterServices();
    }

    private void RegisterServices()
    {
        ServiceLocator.Register<ISplatMapService>(new SplatMapService());
        ServiceLocator.Register<IMapGenerator>(new MapGeneratorService());
        ServiceLocator.Register<IBiomeTexturesService>(biomeTexturesService);
        ServiceLocator.Register<IMap<ChunkObject>>(new Map<ChunkObject>());
        ServiceLocator.Register<IPlayerInfo>(playerInfo);
        ServiceLocator.Register<IWeldMap>(gameObject.AddComponent<WeldMapService>()); 
        ServiceLocator.Register<IRayCastController>(gameObject.AddComponent<RayCastsController>());
        ServiceLocator.Register<ILifeService>(gameObject.AddComponent<LifeService>());
        ServiceLocator.Register<IStaminaService>(gameObject.AddComponent<StaminaService>());
        ServiceLocator.Register<IThirstService>(gameObject.AddComponent<HydrationService>());
        ServiceLocator.Register<IHungerService>(gameObject.AddComponent<HungerService>());
        ServiceLocator.Register<ITemperatureService>(gameObject.AddComponent<TemperatureService>());
        ServiceLocator.Register<IInteractionService>(interactionService);
        ServiceLocator.Register<IPlayerMediator>(gameObject.AddComponent<PlayerMediator>());
        ServiceLocator.Register<IHUDService>(gameObject.AddComponent<HUDHolder>());
        ServiceLocator.Register<IFirstPersonController>(new ControllerReference());
        ServiceLocator.Register<IGameSceneReferences>(gameSceneReferences);
        ServiceLocator.Register<IInventoryService>(inventoryService);
        ServiceLocator.Register<ICraftingService>(craftingService);
        ServiceLocator.Register<ITeleportService>(teleportService);
        ServiceLocator.Register<ITimeService>(timeService);
        ServiceLocator.Register<IUIService>(uiService);
    }
}
}