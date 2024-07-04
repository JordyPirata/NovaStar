using Unity.Mathematics;
namespace Services.Interfaces
{
    public interface INoiseService
    {
        float[] GenerateNoise(float2 coords);
        float[,] GenerateNoise(float2 coords, int width, int height);
    }
}