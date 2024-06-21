using UnityEngine;
using Services;
using Services.Repository;
using Unity.VisualScripting;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

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
        ServiceLocator.Register<ISettingsService>(gameObject.AddComponent<SettingsService>());
        ServiceLocator.Register<IPlayerInfo>(gameObject.AddComponent<PlayerInfo>());
        ServiceLocator.Register<IWeldMap>(gameObject.AddComponent<WeldMap>()); 
        ServiceLocator.Register<IMapGenerator>(new MapGenerator());
        ServiceLocator.Register<IRepository>(new GameRepository());
        ServiceLocator.Register<ISceneLoader>(new SceneLoader());
    }
}