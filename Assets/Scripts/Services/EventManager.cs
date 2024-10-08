using System;
using Services.Interfaces;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Console = UnityEngine.Debug;

namespace Services
{
/// <summary>
/// Load and unload scenes
/// </summary>
public class EventManager : MonoBehaviour, IEventManager
{
    public static Action OnMapLoaded;
    public static Action OnGamePaused;
    public static Action OnGameResumed;
    public static Action OnSceneGameLoaded;
    public static Action OnMenuLoaded;
    public void Start()
    {
        SceneManager.activeSceneChanged += (current, next) =>
        {
            Console.Log($"Scene changed from {current.name} to {next.name}");
        };

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            switch (scene.name)
            {
            case IEventManager.Game:
                OnSceneGameLoaded?.Invoke();
                ServiceLocator.GetService<IPlayerInfo>().StartService();
                ServiceLocator.GetService<IMapGenerator>().StartService();
                ServiceLocator.GetService<IWeldMap>().StartService();
                ServiceLocator.GetService<ILifeService>().StartService();
                ServiceLocator.GetService<IHungerService>().StartService();
                ServiceLocator.GetService<IStaminaService>().StartService();
                ServiceLocator.GetService<IThirstService>().StartService();
                ServiceLocator.GetService<IHUDService>().Initialize();
                Console.Log("Game Scene Loaded");
                break;    
            case IEventManager.MainMenu:
                OnMenuLoaded?.Invoke();
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
            case IEventManager.Game:
                ServiceLocator.GetService<IMapGenerator>().StopService();
                ServiceLocator.GetService<IWeldMap>().StopService();
                ServiceLocator.GetService<IPlayerInfo>().StopService();
                ServiceLocator.GetService<ILifeService>().StopService();
                ServiceLocator.GetService<IHungerService>().StopService();
                ServiceLocator.GetService<IStaminaService>().StopService();
                ServiceLocator.GetService<IThirstService>().StopService();
                Console.Log("Game Scene Unloaded");
                break;    
            case IEventManager.MainMenu:
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