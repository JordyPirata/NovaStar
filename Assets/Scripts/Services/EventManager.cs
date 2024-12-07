using System;
using Services.Interfaces;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Console = UnityEngine.Debug;
using System.Collections.Generic;

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
            case IEventManager.Game1:
            case IEventManager.Game:
                OnSceneGameLoaded?.Invoke();
                List<Type> serviceTypes = new()
                {
                    typeof(IPlayerInfo),
                    typeof(IMapGenerator),
                    typeof(IWeldMap),
                    typeof(ILifeService),
                    typeof(IHungerService),
                    typeof(IStaminaService),
                    typeof(IThirstService),
                    typeof(IHUDService)
                };

                foreach (Type serviceType in serviceTypes)
                {
                    IService service = ServiceLocator.GetService(serviceType) as IService;
                    service?.StartService();
                }
                /*
                ServiceLocator.GetService<IPlayerInfo>().StartService();
                ServiceLocator.GetService<IMapGenerator>().StartService();
                ServiceLocator.GetService<IWeldMap>().StartService();
                ServiceLocator.GetService<ILifeService>().StartService();
                ServiceLocator.GetService<IHungerService>().StartService();
                ServiceLocator.GetService<IStaminaService>().StartService();
                ServiceLocator.GetService<IThirstService>().StartService();
                ServiceLocator.GetService<IHUDService>().Initialize();*/
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
                List<Type> serviceTypes = new()
                {
                    typeof(IPlayerInfo),
                    typeof(IMapGenerator),
                    typeof(IWeldMap),
                    typeof(ILifeService),
                    typeof(IHungerService),
                    typeof(IStaminaService),
                    typeof(IThirstService)
                };

                foreach (Type serviceType in serviceTypes)
                {
                    IService service = ServiceLocator.GetService(serviceType) as IService;
                    service?.StopService();
                }
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