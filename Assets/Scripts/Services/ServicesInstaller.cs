using Services.Interfaces;
using Services.Repository;
using UnityEngine;
using Services;
using UI;
using Services.Player;

[RequireComponent(typeof(FadeController))]

public class ServiceInstaller : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;
    public void Awake()
    {
        DontDestroyOnLoad(this);
        InstallServices();
    }
    public void InstallServices()
    {
        ServiceLocator.Register<IInputActions>(new PlayerInputService());
        ServiceLocator.Register<ISettingsService>(gameObject.AddComponent<SettingsService>());
        ServiceLocator.Register<IPlayerInfo>(gameObject.AddComponent<PlayerInfo>());
        ServiceLocator.Register<IWeldMap>(gameObject.AddComponent<WeldMapService>()); 
        ServiceLocator.Register<IMapGenerator>(new MapGeneratorService());
        ServiceLocator.Register<IRepository>(new GameRepository());
        ServiceLocator.Register<ISceneLoader>(gameObject.AddComponent<SceneLoader>());
        ServiceLocator.Register<IWorldCRUD>(new WorldCRUD());
        ServiceLocator.Register<IWorldData>(gameObject.AddComponent<WorldData>());
        ServiceLocator.Register<INoiseService>(new NoiseServiceShader());
        ServiceLocator.Register<IBiomeDic>(new BiomesDic());
        ServiceLocator.Register<ITextureMapGen>(new TextureMapGen());
        ServiceLocator.Register<IFadeController>(fadeController);
        // new services
        ServiceLocator.Register<IRayCastController>(new RayCastsController());
        ServiceLocator.Register<IPlayerMediator>(gameObject.AddComponent<PlayerMediator>());
        ServiceLocator.Register<ILifeService>(gameObject.AddComponent<LifeService>());
        ServiceLocator.Register<IStaminaService>(gameObject.AddComponent<StaminaService>());
        ServiceLocator.Register<IHydrationService>(gameObject.AddComponent<HydrationService>());
        ServiceLocator.Register<IHungerService>(gameObject.AddComponent<HungerService>());
        ServiceLocator.Register<ITemperatureService>(gameObject.AddComponent<TemperatureService>());
    }
}