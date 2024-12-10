using Unity.Mathematics;
using UnityEngine;

namespace Services.Interfaces
{
    // TODO: Test this class
    public interface IFirstPersonController
    {
        public bool CanMove { get; set; }
        public Transform PlayerTransform { get; }
        bool CanPlane { get; set; }
        void TeleportToPosition(float3 dataTeleportPosition);
        void Stimulate();
    }
}
