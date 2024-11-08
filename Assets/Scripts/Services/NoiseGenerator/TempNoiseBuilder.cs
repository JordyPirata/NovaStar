using Services.Interfaces;
namespace Services.NoiseGenerator
{
public class TempNoiseBuilder : NoiseBuilder, INoiseBuilder
{
    public void Build()
    {
        BuildMatrixNoise();
    }

    public object GetNoise()
    {
        return Noise;
    }

    public void SetKernel()
    {
        kernel = computeShader.FindKernel(Kernel.TempNoise.ToString());
    }
}
}