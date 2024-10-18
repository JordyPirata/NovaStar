using System;
using Services.Interfaces;
using Unity.Mathematics;

namespace Services.NoiseGenerator
{
public class NoiseDirectorService : INoiseDirector
{
    private INoiseBuilder Builder;
    public void SetBuilder(INoiseBuilder builder)
    {
        Builder = builder;
    }
    /// <summary>
    /// Retuns noise based on the builder float[] or float[,]
    /// </summary>
    /// <returns></returns>
    public object MakeNoise(float2 coords)
    {
        switch (Builder)
        {
            case ChunkNoiseBuilder:
                NoiseState noiseState = new()
                {
                    seed = 0,
                    frequency = 0.01f,
                    amplitude = 1f,
                    lacunarity = 2f,
                    gain = 0.5f,
                    octaves = 3,
                    fractalType = FractalType.FBM,
                    noiseType = NoiseType.OpenSimplex2
                };
                Builder.SetCoords(coords);
                Builder.SetState(noiseState);
                Builder.SetKernel();
                Builder.Build();
                return Builder.GetNoise();
            case HumidityNoiseBuilder:
                Builder.SetKernel();
                break;
            default:
                throw new Exception("Invalid Builder");
        }
        return null;
    }
}
}