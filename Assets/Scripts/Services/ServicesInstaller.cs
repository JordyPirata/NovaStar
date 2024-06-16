using UnityEngine;
using Services;
using Services.Repository;

public class ServiceInstaller : MonoBehaviour
{
    public void Awake()
    {
        InstallServices();
        // DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);
    }
    public void InstallServices()
    {
        ServiceLocator.Register<ISettingsService>(new SettingsService());
        ServiceLocator.Register<IWeldMap>(new WeldMap());
        ServiceLocator.Register<IPlayerInfo>(gameObject.GetComponent<PlayerInfo>());
        ServiceLocator.Register<IMapGenerator>(new MapGenerator());
        ServiceLocator.Register<IRepository>(new GameRepository());
    }
    private void Start()
    {
        var mapGenerator = ServiceLocator.GetService<IMapGenerator>();
        var weldMap = ServiceLocator.GetService<IWeldMap>();

        StartCoroutine(weldMap.WeldChunks());
        mapGenerator.GenerateMap();
    }
}