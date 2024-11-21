using Services.Interfaces;
namespace Services.NoiseGenerator
{
public class TempChunkNoiseBuilder : NoiseBuilder, INoiseBuilder
{
    public void Build()
    {
        BuildArrayNoise();
    }

    public object GetNoise()
    {
        return Noise;
    }

    public void SetKernel()
    {
        kernel = computeShader.FindKernel(Kernel.HumidityNoise.ToString());
    }
}
}