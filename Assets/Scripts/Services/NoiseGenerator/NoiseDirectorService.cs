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
    public object GetNoise(float2 coords)
    {
        switch (Builder)
        {
            case ChunkNoiseBuilder:

                NoiseState noiseState = new();
                Builder.SetCoords(coords);
                Builder.SetState(noiseState);
                Builder.SetKernel();
                Builder.Build();
                return Builder.GetNoise();

            case HumidityNoiseBuilder:

                NoiseState humidityState = new();
                Builder.SetCoords(coords);
                Builder.SetSize(TextureMapGen.Width, TextureMapGen.Depth);
                Builder.SetState(humidityState);
                Builder.SetKernel();
                Builder.Build();
                return Builder.GetNoise();

            case TempNoiseBuilder:

                NoiseState tempState = new();
                Builder.SetSize(TextureMapGen.Width, TextureMapGen.Depth);
                Builder.SetCoords(coords - 1);
                Builder.SetState(tempState);
                Builder.SetKernel();
                Builder.Build();
                return Builder.GetNoise();
            
            case TempChunkNoiseBuilder:

                NoiseState tempChunkState = new();
                Builder.SetCoords(coords);
                Builder.SetState(tempChunkState);
                Builder.SetKernel();
                Builder.Build();
                return Builder.GetNoise();

            case HumidityChunkNoiseBuilder:

                NoiseState humidityChunkState = new();
                Builder.SetCoords(coords);
                Builder.SetState(humidityChunkState);
                Builder.SetKernel();
                Builder.Build();
                return Builder.GetNoise();
            
            default:
                throw new Exception("Invalid Builder");
        }
    }
}
}