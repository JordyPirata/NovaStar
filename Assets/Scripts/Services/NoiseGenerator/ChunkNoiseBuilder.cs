using System;
using System.Diagnostics;
using System.Drawing;
using Services.Interfaces;
using Debug = UnityEngine.Debug;
namespace Services.NoiseGenerator
{
public class ChunkNoiseBuilder : NoiseBuilder,  INoiseBuilder<float[]>
{
    public void SetKernel()
    {
        kernel = computeShader.FindKernel("ChunkNoiseBuilder");
    }
    public void Build()
    {
        GenerateNoise();
    }

    public float[] GetNoise()
    {
        return (float[])Noise;
    }

    public override void SetSize(int width, int depth)
    {
        throw new NotSupportedException("This method is not supported for this class");
    }
}
}