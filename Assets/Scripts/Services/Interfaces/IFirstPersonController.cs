using UnityEngine;

namespace Services.Interfaces
{
    // TODO: Test this class
    public interface IFirstPersonController
    {
        public FirstPersonCharacter ControllerScript { get; }
        public Transform PlayerTransform { get; }
    }
}
