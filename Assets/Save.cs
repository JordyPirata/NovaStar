using Services;
using UnityEngine;
using Services.Interfaces;
using Services.Player;
using UnityEditor.SearchService;
using System.IO;
using Services.Installer;


public class Save : MonoBehaviour
{
    // Add buton to change the scene

    // Start is called before the first frame update
    public void Awake()
    {
        ServiceLocator.Register<IInputActions>(new PlayerInputService());
        ServiceLocator.Register<IEventManager>(gameObject.AddComponent<EventManager>());
    }
    public void ChangeScene()
    {
        Debug.Log("Change Scene");
        ServiceLocator.GetService<IEventManager>().LoadScene(IEventManager.DemoScene);
        
    }

}
