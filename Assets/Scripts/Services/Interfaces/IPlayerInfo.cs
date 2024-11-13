using Services.Player;
using Unity.Mathematics;
using UnityEngine;

namespace Services.Interfaces
{
    public interface IPlayerInfo
    {
        Transform PlayerTransform();
        float3 PlayerPosition();
        float2 PlayerCoordinate();
        float MapHeight();
        float MapTemperature();
        float MapHumidity();
        void StartService();
        void StopService();
        void PlayerDied();

        /* PlayerMediator GetPlayerMediator();*/
    }
}