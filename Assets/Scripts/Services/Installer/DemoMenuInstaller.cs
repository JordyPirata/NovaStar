using Services;
using UnityEngine;
using Services.Interfaces;
using Services.Player;
using UnityEditor.SearchService;
using System.IO;
using Services.Installer;


public class DemoMenuInstaller : MonoBehaviour
{
    // Add buton to change the scene

    // Start is called before the first frame update
    public void Awake()
    {
        DontDestroyOnLoad(this);
        
        ServiceLocator.Register<IInputActions>(new PlayerInputService());
        ServiceLocator.Register<IEventManager>(gameObject.AddComponent<EventManager>());
    }
}
