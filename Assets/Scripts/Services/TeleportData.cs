using System;
using System.Numerics;
using Unity.Mathematics;

namespace Services
{
    [Serializable]
    public class TeleportData
    {
        public string teleportName;
        public float3 teleportPosition;
    }
}