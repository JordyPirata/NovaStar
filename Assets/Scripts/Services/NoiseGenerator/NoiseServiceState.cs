using System;
using Services.NoiseGenerator;

namespace Services
{
    public class NoiseState
    {
        public int seed = 1337;
        public NoiseType noiseType = NoiseType.OpenSimplex2;
        public FractalType fractalType = FractalType.FBM;
        public int octaves = 4;
        public float frequency = 0.001f;
        public float lacunarity = 2.0f;
        public float gain = 0.5f;
        public float amplitude = 0.5f;
        public float distance = 0.5f;
    }
}
