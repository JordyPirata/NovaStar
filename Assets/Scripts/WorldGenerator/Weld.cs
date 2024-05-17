using Unity.Mathematics;
using UnityEngine;
using Generator;

public class Weld
{
    public static void SetNeighbors(ChunkGrid<ChunkObject> chunkGrid,float2 coord)
    {
        // Set the neighbors of the chunk
        ChunkObject thisChunk = chunkGrid[coord];

        Terrain leftNeighbor;
    }
}