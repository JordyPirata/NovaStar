using Unity.Mathematics;

namespace Services.Interfaces
{
    public interface IPlayerInfo
    {
        void Init();
        float3 GetPlayerPosition();
        float2 GetPlayerCoordinate();
        void StartService();
        void StopService();

        void GroundPlayer();
    }
}