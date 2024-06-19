using Unity.Mathematics;

interface IPlayerInfo
{
    void Init(UnityEngine.Transform player);
    float2 GetPlayerPosition();
    float2 GetPlayerCoordinate();
}