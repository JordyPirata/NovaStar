using System;
using Config;

namespace Services
{
    public class NoiseServiceState
    {
        public int seed = 1337;
        public NoiseType noiseType = NoiseType.OpenSimplex2;
        public FractalType fractalType = FractalType.None;
        public int octaves = 3;
        public float frequency = 0.01f;
        public float lacunarity = 2.0f;
        public float gain = 0.5f;
    }
}