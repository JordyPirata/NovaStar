using Services.Interfaces;
namespace Services.NoiseGenerator
{
public class TempNoiseBuilder : NoiseBuilder, INoiseBuilder
{
    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public object GetNoise()
    {
        return Noise;
    }

    public void SetKernel()
    {
        throw new System.NotImplementedException();
    }
    }
}