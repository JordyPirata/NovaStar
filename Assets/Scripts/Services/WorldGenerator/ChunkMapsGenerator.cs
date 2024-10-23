using Services.NoiseGenerator;
using Services.Interfaces;
using Unity.Mathematics;
namespace Services.WorldGenerator
{

public class ChunkMapsGenerator
{
    private INoiseDirector NoiseDirector => ServiceLocator.GetService<INoiseDirector>();

    public void HumidityMap(float2 chunkCoords)
    {
        NoiseDirector.SetBuilder(new HumidityNoiseBuilder());
        NoiseDirector.GetNoise(chunkCoords);
    }
    public void TemperatureMap(float2 chunkCoords)
    {
        NoiseDirector.SetBuilder(new TempChunkNoiseBuilder());
        NoiseDirector.GetNoise(chunkCoords);
    }
}
}