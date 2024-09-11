using Services.Player;
using Unity.Mathematics;
using UnityEngine;

namespace Services.Interfaces
{
    public interface IPlayerInfo
    {
        void Init();
        Transform PlayerTransform();
        float3 PlayerPosition();
        float2 PlayerCoordinate();
        void StartService();
        void StopService();
        void PlayerDied();

        /* PlayerMediator GetPlayerMediator();*/
    }
}