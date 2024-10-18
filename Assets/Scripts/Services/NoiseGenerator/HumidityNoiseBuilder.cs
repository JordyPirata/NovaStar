using Services.Interfaces;
namespace Services.NoiseGenerator
{
public class HumidityNoiseBuilder : NoiseBuilder, INoiseBuilder
{
    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public object GetNoise()
    {
        return (float[,])Noise;
    }

    public void SetKernel()
    {
        throw new System.NotImplementedException();
    }
    }
}