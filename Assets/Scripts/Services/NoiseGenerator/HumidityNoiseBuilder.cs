using Services.Interfaces;
namespace Services.NoiseGenerator
{
public class HumidityNoiseBuilder : NoiseBuilder, INoiseBuilder<float[,]>
{
    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public float[,] GetNoise()
    {
        return (float[,])Noise;
    }

    public void SetKernel()
    {
        throw new System.NotImplementedException();
    }
    }
}