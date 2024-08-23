using Models;
using Unity.Mathematics;

namespace Services.Interfaces
{
    public interface IWorldData
    {
        string GetDirectory();        
        string GetName();
        string GetPath();
        int GetSeed();
        int2 GetHumidityRange();
        int2 GetTemperatureRange();
        void SetHumidityRange(int2 humidityRange);
        void SetTemperatureRange(int2 temperatureRange);
        void SetSeed(int seed);
        void SetWorld(World world);
        void UpdateWorld();
        void SaveWorld();
        bool SetIsGenerated(bool isGenerated);
        bool GetIsGenerated();
    }
}