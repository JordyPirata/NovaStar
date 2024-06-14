using UnityEngine;
using Services;
using Services.Repository;

public class ServiceInstaller : MonoBehaviour
{
    private void Awake()
    {
        InstallServices();
    }
    public void InstallServices()
    {
        ServiceLocator.Register<ISettingsService>(new SettingsService());
        ServiceLocator.Register<IWeldMap>(new WeldMap());
        ServiceLocator.Register<IPlayerInfo>(gameObject.AddComponent<PlayerInfo>());
        ServiceLocator.Register<IMapGenerator>(new MapGenerator());
        ServiceLocator.Register<IRepository>(new GameRepository());
    }
}