using UnityEngine;
using Services.Interfaces;

namespace Services
{
    public class ControllerReference : IFirstPersonController
    {
        public FirstPersonCharacter ControllerScript 
        { 
            get => ControllerScript? ControllerScript : GameObject.Find("Player").GetComponent<FirstPersonCharacter>();
            set => ControllerScript = value;
        }
        public Transform PlayerTransform 
        { 
            get => ControllerScript.Controller.transform;
        }
        public bool Sprinting 
        { 
            get => ControllerScript.sprinting;  
            set => ControllerScript.sprinting = value; 
        }
    }
}
