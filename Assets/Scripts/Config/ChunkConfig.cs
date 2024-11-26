namespace Config
{
    public static class ChunkConfig
    {
        public const int width = 129;
        public const int depth = 129;
        public static int Length => width * depth;
        public const float maxViewDst = 750;
        public static int Height = 150;
    }
}