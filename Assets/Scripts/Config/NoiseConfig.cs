namespace Config
{
    public static class ChunkConfig
    {
        public const int width = 257;
        public const int depth = 257;
        public static int Length => width * depth;
        public static int Height = 150;
        public static int Seed = 1337;
        public static float Frequency = 0.01f;
        public static NoiseType noiseType = NoiseType.OpenSimplex2;
        public static int noiseOctaves = 3;
        public const float maxViewDst = 750;
        public static FractalType FractalType = FractalType.None;
    }
}