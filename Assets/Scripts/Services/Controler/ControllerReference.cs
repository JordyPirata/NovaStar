using UnityEngine;
using Services.Interfaces;

namespace Services
{
    public class ControllerReference : IFirstPersonController
    {
        public ControllerReference()
        {
            EventManager.OnSceneGameLoaded += () =>
            {
                
                ControllerScript = GameObject.Find("Player").GetComponent<FirstPersonCharacter>();
                if (ControllerScript == null)
                {
                    throw new System.Exception("ControllerReference: Controller not found");
                }
            };
        }
        public FirstPersonCharacter ControllerScript { get; set; }
        public Transform PlayerTransform 
        {
            get => ControllerScript.Controller.transform;
        }
    }
}
