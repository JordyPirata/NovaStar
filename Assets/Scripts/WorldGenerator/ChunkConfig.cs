using System.Collections;
using UnityEngine;
using Unity.Mathematics;
using System.Threading.Tasks;

namespace WorldGenerator
{
//TODO: Change name to ChunkFacade and implement 
public static class ChunkConfig
{
    public const int width = 257;
    public const int depth = 257;
    public static int Length => width * depth;
    public static int height = 150;
    public static int seed = 6551445;
    public const float maxViewDst = 750;
}
}