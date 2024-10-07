using UnityEngine;
using Services.Interfaces;

namespace Services
{
    public class ControllerReference : IFirstPersonController
    {
        private FirstPersonCharacter _controllerScript;
        public FirstPersonCharacter ControllerScript 
        { 
            get => _controllerScript? ControllerScript : GameObject.Find("Player").GetComponent<FirstPersonCharacter>();
            set => _controllerScript = value;
        }
        public Transform PlayerTransform 
        {
            get => ControllerScript.Controller.transform;
        }
    }
}
