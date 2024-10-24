using Services.Interfaces;
using Services.Repository;
using UnityEngine;
using Services;
using UI;
using Services.WorldGenerator;
using Services.Player;
using Services.NoiseGenerator;
using Services.Splatmap;

[RequireComponent(typeof(FadeController))]
[RequireComponent(typeof(PlayerMediatorData))]
[RequireComponent(typeof(BiomeTexturesService))]

public class ServiceInstaller : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;
    [SerializeField] private PlayerMediatorData playerMediatorData;
    [SerializeField] private BiomeTextures biomeTextures;
    public void Awake()
    {
        DontDestroyOnLoad(this);
        InstallServices();
    }
    public void InstallServices()
    {
        ServiceLocator.Register<IMap<ChunkObject>>(new Map<ChunkObject>());
        ServiceLocator.Register<IInputActions>(new PlayerInputService());
        ServiceLocator.Register<ISettingsService>(gameObject.AddComponent<SettingsService>());
        ServiceLocator.Register<IPlayerInfo>(gameObject.AddComponent<PlayerInfo>());
        ServiceLocator.Register<IWeldMap>(gameObject.AddComponent<WeldMapService>()); 
        ServiceLocator.Register<IMapGenerator>(new MapGeneratorService());
        // ServiceLocator.Register<IMapGenerator>(gameObject.AddComponent<MapGeneratorC>());
        ServiceLocator.Register<IRepository>(new GameRepository());
        ServiceLocator.Register<IEventManager>(gameObject.AddComponent<EventManager>());
        ServiceLocator.Register<IWorldCRUD>(new WorldCRUD());
        ServiceLocator.Register<IWorldData>(gameObject.AddComponent<WorldData>());
        ServiceLocator.Register<INoiseDirector>(new NoiseDirectorService()); // new service
        ServiceLocator.Register<IBiomeDic>(new BiomesDic());
        ServiceLocator.Register<ITextureMapGen>(new TextureMapGen());
        ServiceLocator.Register<IFadeController>(fadeController);
        ServiceLocator.Register<IRayCastController>(gameObject.AddComponent<RayCastsController>());
        ServiceLocator.Register<ILifeService>(gameObject.AddComponent<LifeService>());
        ServiceLocator.Register<IStaminaService>(gameObject.AddComponent<StaminaService>());
        ServiceLocator.Register<IThirstService>(gameObject.AddComponent<HydrationService>());
        ServiceLocator.Register<IHungerService>(gameObject.AddComponent<HungerService>());
        ServiceLocator.Register<ITemperatureService>(gameObject.AddComponent<TemperatureService>());
        ServiceLocator.Register<IInteractionService>(gameObject.AddComponent<InteractionService>());
        var playerMediator = gameObject.AddComponent<PlayerMediator>();
        ServiceLocator.Register<IPlayerMediator>(playerMediator);
        playerMediator.Configure(playerMediatorData);
        // new services
        ServiceLocator.Register<IHUDService>(gameObject.AddComponent<HUDHolder>());
        ServiceLocator.Register<IFirstPersonController>(new ControllerReference());
        ServiceLocator.Register<INoiseDirector>(new NoiseDirectorService());
        ServiceLocator.Register<ISplatMapService>(new SplatMapService());
        var biomeTexturesService = new BiomeTexturesService();
        biomeTexturesService.Configure(biomeTextures);
        ServiceLocator.Register<IBiomeTexturesService>(biomeTexturesService);
    }
}