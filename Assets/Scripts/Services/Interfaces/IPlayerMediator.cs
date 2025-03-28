using Models;
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
        void Dehydrate(int i);
        void LimitStamina(int i);
        void LoseLife(int i);
        void EquipCoat(bool b);
        void StopLifeRegen(bool b);
        void LoadPlayerStats(PlayerStatsModel playerStatsModel);
        PlayerStatsModel GetPlayerStatsModel();
        void ChangeController(bool thirdPersonCamera);
    }
}