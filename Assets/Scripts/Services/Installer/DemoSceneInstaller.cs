using Player.Gameplay.Items;
using Services.Interfaces;
using Services.Player;
using Services.Splatmap;
using Services.WorldGenerator;
using UnityEngine;

namespace Services.Installer
{
public class DemoSceneInstaller : MonoBehaviour
{
    [SerializeField] private HUDHolder hudHolder;
    [SerializeField] private InventoryService inventoryService;
    [SerializeField] private CraftingService craftingService;
    [SerializeField] private TeleportService teleportService;
    [SerializeField] private DropsService dropsService;
    [SerializeField] private UIService uiService;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private InteractionService interactionService;
    [SerializeField] private ControllerForDemo firstPersonCharacter;
    [SerializeField] private JetPackService jetPackService;
    [SerializeField] private HoverBoardService hoverBoardService;
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
        
        ServiceLocator.Register<IPlayerInfo>(playerInfo);
        ServiceLocator.Register<IRayCastController>(gameObject.AddComponent<RayCastsController>());
        ServiceLocator.Register<ILifeService>(gameObject.AddComponent<LifeService>());
        ServiceLocator.Register<IStaminaService>(gameObject.AddComponent<StaminaService>());
        ServiceLocator.Register<IThirstService>(gameObject.AddComponent<HydrationService>());
        ServiceLocator.Register<IHungerService>(gameObject.AddComponent<HungerService>());
        ServiceLocator.Register<IInteractionService>(interactionService);
        ServiceLocator.Register<IPlayerMediator>(gameObject.AddComponent<PlayerMediator>());
        ServiceLocator.Register<IHUDService>(hudHolder);
        ServiceLocator.Register<IFirstPersonController>(firstPersonCharacter);
        ServiceLocator.Register<IInventoryService>(inventoryService);
        ServiceLocator.Register<ICraftingService>(craftingService);
        ServiceLocator.Register<ITeleportService>(teleportService);
        ServiceLocator.Register<IUIService>(uiService);
        ServiceLocator.Register<IEquipablesService>(new EquipablesService());
        ServiceLocator.Register<IJetPackService>(jetPackService);
        ServiceLocator.Register<IHoverboardService>(hoverBoardService);
        
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
        ServiceLocator.UnRegister<IPlayerAnimator>();
    }
}
}