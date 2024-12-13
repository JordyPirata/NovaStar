using Player.Gameplay.Items;
using Services.Interfaces;
using Services.Player;
using Services.Splatmap;
using Services.WorldGenerator;
using UnityEngine;

namespace Services.Installer
{
[RequireComponent(typeof(PlayerMediatorData))]
public class GameSceneInstaller : MonoBehaviour
{
    [SerializeField] private HUDHolder hudHolder;
    [SerializeField] private PlayerMediatorData playerMediatorData;
    [SerializeField] private InventoryService inventoryService;
    [SerializeField] private CraftingService craftingService;
    [SerializeField] private TeleportService teleportService;
    [SerializeField] private TimeService timeService;
    [SerializeField] private DropsService dropsService;
    [SerializeField] private UIService uiService;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private InteractionService interactionService;
    [SerializeField] private BiomeTexturesService biomeTexturesService;
    [SerializeField] private FirstPersonCharacter firstPersonCharacter;
    [SerializeField] private JetPackService jetPackService;
    [SerializeField] private HoverBoardService hoverBoardService;
    [SerializeField] private SaveGameSceneService saveGameSceneService;
    private void Awake()
    {
        RegisterServices();
    }
    private void OnDestroy()
    {
        UnRegisterServices();
    }

    private void RegisterServices()
    {
        
        ServiceLocator.Register<ISplatMapService>(new SplatMapService());
        ServiceLocator.Register<IMapGenerator>(gameObject.AddComponent<MapGeneratorService>());
        ServiceLocator.Register<IBiomeTexturesService>(biomeTexturesService);
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
        ServiceLocator.Register<IHUDService>(hudHolder);
        ServiceLocator.Register<IFirstPersonController>(firstPersonCharacter);
        ServiceLocator.Register<IInventoryService>(inventoryService);
        ServiceLocator.Register<ICraftingService>(craftingService);
        ServiceLocator.Register<ITeleportService>(teleportService);
        ServiceLocator.Register<ITimeService>(timeService);
        ServiceLocator.Register<IUIService>(uiService);
        ServiceLocator.Register<IEquipablesService>(new EquipablesService());
        ServiceLocator.Register<IJetPackService>(jetPackService);
        ServiceLocator.Register<IHoverboardService>(hoverBoardService);
        ServiceLocator.Register<ISaveGameSceneService>(saveGameSceneService);
    }
    private void UnRegisterServices()
    {
        ServiceLocator.UnRegister<ISplatMapService>();
        ServiceLocator.UnRegister<IMapGenerator>();
        ServiceLocator.UnRegister<IBiomeTexturesService>();
        ServiceLocator.UnRegister<IPlayerInfo>();
        ServiceLocator.UnRegister<IWeldMap>();
        ServiceLocator.UnRegister<IRayCastController>();
        ServiceLocator.UnRegister<ILifeService>();
        ServiceLocator.UnRegister<IStaminaService>();
        ServiceLocator.UnRegister<IThirstService>();
        ServiceLocator.UnRegister<IHungerService>();
        ServiceLocator.UnRegister<ITemperatureService>();
        ServiceLocator.UnRegister<IInteractionService>();
        ServiceLocator.UnRegister<IPlayerMediator>();
        ServiceLocator.UnRegister<IHUDService>();
        ServiceLocator.UnRegister<IFirstPersonController>();
        ServiceLocator.UnRegister<IGameSceneReferences>();
        ServiceLocator.UnRegister<IInventoryService>();
        ServiceLocator.UnRegister<ICraftingService>();
        ServiceLocator.UnRegister<ITeleportService>();
        ServiceLocator.UnRegister<ITimeService>();
        ServiceLocator.UnRegister<IUIService>();
        ServiceLocator.UnRegister<IEquipablesService>();
        ServiceLocator.UnRegister<IJetPackService>();
        ServiceLocator.UnRegister<IHoverboardService>();
    }
}
}