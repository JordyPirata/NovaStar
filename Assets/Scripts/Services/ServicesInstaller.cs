using UnityEngine;
using Services;
using Services.Repository;
using Unity.VisualScripting;

public class ServiceInstaller : MonoBehaviour
{
    // Register service On Load second scene
    public void Awake()
    {
        // DontDestroyOnLoad(gameObject)
        DontDestroyOnLoad(this);
        InstallServices();        
    }
    public void InstallServices()
    {
        ServiceLocator.Register<ISettingsService>(gameObject.AddComponent<SettingsService>());
        ServiceLocator.Register<IPlayerInfo>(gameObject.AddComponent<PlayerInfo>());
        ServiceLocator.Register<IWeldMap>(new WeldMap()); 
        ServiceLocator.Register<IMapGenerator>(new MapGenerator());
        ServiceLocator.Register<IRepository>(new GameRepository());
    }
    public void RegisterService<T>(T service) => ServiceLocator.Register(service);
}