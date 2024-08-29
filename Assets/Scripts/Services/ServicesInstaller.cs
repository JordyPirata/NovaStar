using Services.Interfaces;
using Services.Repository;
using UnityEngine;
using Services;

public class ServiceInstaller : MonoBehaviour
{
    public void Awake()
    {
        // DontDestroyOnLoad(gameObject)
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
        
    }
}
