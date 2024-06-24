using System.Collections;
using UnityEngine;
using Unity.Mathematics;
using System.Threading.Tasks;

public static class ChunkConfig
{
    public const int width = 257;
    public const int depth = 257;
    public static int Length => width * depth;
    public static int height = 150;
    public static int seed;
    public const float maxViewDst = 750;
}