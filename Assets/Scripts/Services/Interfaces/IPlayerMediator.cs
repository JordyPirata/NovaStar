using Services.Player;
using Unity.Mathematics;
using UnityEngine;
using Util;

namespace Services.Interfaces
{
    public interface IPlayerMediator
    {
        void MapLoaded();
        void TeleportToPosition(float3 dataTeleportPosition);
        bool UseConsumable(ConsumableType consumableType);
        bool IsTired { get;}
    }
}