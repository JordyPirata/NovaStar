using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services.Interfaces;

public class ButtonScript : MonoBehaviour
{
    IEventManager eventManager;
    // Start is called before the first frame update
    void Start()
    {
        eventManager = ServiceLocator.GetService<IEventManager>();
    }

    public void ChangeScene()
    {
        eventManager.LoadScene(IEventManager.DemoScene);
    }
}
