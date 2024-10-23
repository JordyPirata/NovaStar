using Services.Interfaces;

namespace Services.NoiseGenerator
{
public class HumidityChunkNoiseBuilder : NoiseBuilder, INoiseBuilder
{
    public void SetKernel()
    {
        kernel = computeShader.FindKernel(Kernel.ChunkNoise.ToString());
    }
    public void Build()
    {
        BuildArrayNoise();
    }

    public object GetNoise()
    {
        return Noise;
    }

    
}
}
