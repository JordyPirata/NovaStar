using Unity.Mathematics;

interface IPlayerInfo
{
    void Init();
    float2 GetPlayerPosition();
    float2 GetPlayerCoordinate();
    void StartService();
    void StopService();

}