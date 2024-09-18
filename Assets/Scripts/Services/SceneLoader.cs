using Services.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Console = UnityEngine.Debug;

namespace Services
{
/// <summary>
/// Load and unload scenes
/// </summary>
public class SceneLoader : MonoBehaviour, ISceneLoader
{
    public const string GameScene = "Game";
    public const string MenuScene = "MainM";
    public void Start()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            switch (scene.name)
            {
            case GameScene:
                ServiceLocator.GetService<IPlayerInfo>().Init();
                ServiceLocator.GetService<IPlayerInfo>().StartService();
                ServiceLocator.GetService<IMapGenerator>().StartService();
                ServiceLocator.GetService<IWeldMap>().StartService();
                ServiceLocator.GetService<ILifeService>().StartService();
                ServiceLocator.GetService<IHungerService>().StartService();
                ServiceLocator.GetService<IStaminaService>().StartService();
                ServiceLocator.GetService<IThirstService>().StartService();
                Console.Log("Game Scene Loaded");
                break;    
            case MenuScene:
                Console.Log("Menu Scene Loaded");
                // Add code here to initialize your services for the "Menu" scene
                // For example:d
                // ServiceC.Initialize();    
                break;
            }
        };
        SceneManager.sceneUnloaded += (scene) =>
        {
            switch (scene.name)
            {
            case GameScene:
                ServiceLocator.GetService<IMapGenerator>().StopService();
                ServiceLocator.GetService<IWeldMap>().StopService();
                ServiceLocator.GetService<IPlayerInfo>().StopService();
                ServiceLocator.GetService<ILifeService>().StopService();
                ServiceLocator.GetService<IHungerService>().StopService();
                ServiceLocator.GetService<IStaminaService>().StopService();
                ServiceLocator.GetService<IThirstService>().StopService();
                Console.Log("Game Scene Unloaded");
                break;    
            case MenuScene:
                Console.Log("Menu Scene Unloaded");
                // Add code here to initialize your services for the "Menu" scene
                // For example:
                // ServiceC.Initialize();    
                break;
            }
            
        };
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        
    }
}
}