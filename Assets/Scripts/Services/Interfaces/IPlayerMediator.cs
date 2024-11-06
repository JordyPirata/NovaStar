using Services.Player;
using Unity.Mathematics;
using UnityEngine;

namespace Services.Interfaces
{
    public interface IPlayerMediator
    {
        void MapLoaded();
        void GoToPosition(float3 dataTeleportPosition);
    }
}