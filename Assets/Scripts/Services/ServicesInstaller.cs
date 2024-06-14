using UnityEngine;
using Services;
using Services.Repository;

public class ServiceInstaller : MonoBehaviour
{
    private void Awake()
    {
        InstallServices();
        gameObject.AddComponent<PlayerInfo>();
    }
    public void InstallServices()
    {
        ServiceLocator.Register<ISettingsService>(new SettingsService());
        ServiceLocator.Register<IWeldMap>(new WeldMap());
        ServiceLocator.Register<IPlayerInfo>(new PlayerInfo());
        ServiceLocator.Register<IMapGenerator>(new MapGenerator());
        ServiceLocator.Register<IRepository>(new GameRepository());
    }
}