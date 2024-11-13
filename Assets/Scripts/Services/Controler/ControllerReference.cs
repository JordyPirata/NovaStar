using System.Collections;
using UnityEngine;
using Services.Interfaces;
using Unity.Mathematics;
using UnityEditor.Localization.Plugins.XLIFF.V20;

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

        public void TeleportToPosition(float3 dataTeleportPosition)
        {
            ControllerScript.TeleportToPosition(dataTeleportPosition);
        }

    }
}
