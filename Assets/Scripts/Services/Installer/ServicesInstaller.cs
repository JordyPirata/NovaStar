using Services.Interfaces;
using Services.NoiseGenerator;
using Services.Repository;
using Services.WorldGenerator;
using UI;
using UnityEngine;

namespace Services.Installer
{
    [RequireComponent(typeof(FadeController))]
    [RequireComponent(typeof(BiomeTexturesService))]

    public class ServiceInstaller : MonoBehaviour
    {
        [SerializeField] private FadeController fadeController;
    
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
        ServiceLocator.Register<ICoroutineManager>(gameObject.AddComponent<CoroutineManager>());
        // ServiceLocator.Register<IMapGenerator>(gameObject.AddComponent<MapGeneratorC>());
        ServiceLocator.Register<IRepository>(new GameRepository());
        ServiceLocator.Register<IEventManager>(gameObject.AddComponent<EventManager>());
        ServiceLocator.Register<IWorldCRUD>(new WorldCRUD());
        ServiceLocator.Register<IWorldData>(gameObject.AddComponent<WorldData>());
        ServiceLocator.Register<INoiseDirector>(new NoiseDirectorService()); // new service
        ServiceLocator.Register<IBiomeDic>(new BiomesDic());
        ServiceLocator.Register<ITextureMapGen>(new TextureMapGen());
        ServiceLocator.Register<IFadeController>(fadeController);
        ServiceLocator.Register<INoiseDirector>(new NoiseDirectorService());
        
        }
    }
}