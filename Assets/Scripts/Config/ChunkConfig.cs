namespace Config
{
    public static class ChunkConfig
    {
        public const int width = 257;
        public const int depth = 257;
        public static int Length => width * depth;
        public const float maxViewDst = 750;
        public static int Height = 150;
    }
}