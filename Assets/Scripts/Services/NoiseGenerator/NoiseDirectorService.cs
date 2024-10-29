using System;
using Services.Interfaces;
using Unity.Mathematics;

namespace Services.NoiseGenerator
{
public class NoiseDirectorService : INoiseDirector
{
    private INoiseBuilder Builder;
    private NoiseState State;
    public void SetBuilder(INoiseBuilder builder)
    {
        Builder = builder;
        State = null;
    }
    public void SetExternalState(NoiseState state)
    {
        State = state;
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
                // if SetExternalState is not set create a default state
                State ??= new NoiseState();

                Builder.SetCoords(coords);
                Builder.SetState(State);
                Builder.SetKernel();
                Builder.Build();

                return Builder.GetNoise();

            case HumidityNoiseBuilder:
            
                State ??= new NoiseState();

                Builder.SetCoords(coords);
                Builder.SetSize(TextureMapGen.Width, TextureMapGen.Depth);
                Builder.SetState(State);
                Builder.SetKernel();
                Builder.Build();

                return Builder.GetNoise();

            case TempNoiseBuilder:

                State ??= new NoiseState();

                Builder.SetSize(TextureMapGen.Width, TextureMapGen.Depth);
                Builder.SetCoords(coords);
                Builder.SetState(State);
                Builder.SetKernel();
                Builder.Build();

                return Builder.GetNoise();
            
            case TempChunkNoiseBuilder:

                State ??= new NoiseState();

                Builder.SetCoords(coords);
                Builder.SetState(State);
                Builder.SetKernel();
                Builder.Build();

                return Builder.GetNoise();

            case HumidityChunkNoiseBuilder:

                State ??= new NoiseState();
                
                Builder.SetCoords(coords);
                Builder.SetState(State);
                Builder.SetKernel();
                Builder.Build();

                return Builder.GetNoise();
            
            default:
                throw new Exception("Invalid Builder");
        }
    }

    
}
}