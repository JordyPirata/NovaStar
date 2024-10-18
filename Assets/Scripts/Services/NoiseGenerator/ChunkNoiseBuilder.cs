using System;
using System.Diagnostics;
using System.Drawing;
using Services.Interfaces;
using Debug = UnityEngine.Debug;
namespace Services.NoiseGenerator
{
public class ChunkNoiseBuilder : NoiseBuilder,  INoiseBuilder
{
    public void SetKernel()
    {
        kernel = computeShader.FindKernel(Kernel.ChunkNoise.ToString());
    }
    public void Build()
    {
        BuildMatrixNoise();
    }

    public object GetNoise()
    {
        return Noise;
    }

    public override void SetSize(int width, int depth)
    {
        throw new NotSupportedException("This method is not supported for this class");
    }
}
}