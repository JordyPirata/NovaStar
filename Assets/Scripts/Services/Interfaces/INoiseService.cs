using Unity.Mathematics;
namespace Services.Interfaces
{
    public interface INoiseService
    {
        /// <summary>
        ///  Generate noise for the chunk
        /// </summary>
        float[] GenerateNoise(float2 coords);
        /// <summary>
        /// Generate noise Map
        /// </summary> 
        float[,] GenerateNoise(float2 coords, int width, int height);
    }
}