using Unity.Mathematics;
using UnityEngine;
using WorldGenerator;
using Map = WorldGenerator.ChunkGrid<WorldGenerator.ChunkObject>;
using Unity.Burst;
using Unity.VisualScripting;
using System.Collections;

namespace Services
{
/// <summary>
/// This class is responsible for welding the chunks together
/// </summary>
[BurstCompile]
public struct WeldMap: IWeldMap
{
    public readonly IEnumerator WeldChunks()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            SetNeighborsAll();
        }
    }
    private static void SetNeighbors(float2 coord)
    {
        // Set the neighbors of the chunk
        ChunkObject thisChunk = Map.Instance[coord];
        Terrain terrain = thisChunk.Terrain;

        Terrain leftNeighbor = Map.Instance[coord + new float2(-1, 0)]?.Terrain;
        if (!leftNeighbor.IsUnityNull() && leftNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(leftNeighbor, terrain.topNeighbor, terrain.rightNeighbor, terrain.bottomNeighbor);
            leftNeighbor.SetNeighbors(leftNeighbor.leftNeighbor, leftNeighbor.topNeighbor, terrain, leftNeighbor.bottomNeighbor);
        }
        
        Terrain rightNeighbor = Map.Instance[ coord + new float2(+1, 0) ]?.Terrain;
        if (!rightNeighbor.IsUnityNull() && rightNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, terrain.topNeighbor, rightNeighbor, terrain.bottomNeighbor);
            rightNeighbor.SetNeighbors(terrain, rightNeighbor.topNeighbor, rightNeighbor.rightNeighbor, rightNeighbor.bottomNeighbor);
        }

        Terrain bottomNeighbor = Map.Instance[ coord + new float2(0, -1) ]?.Terrain;
        if (!bottomNeighbor.IsUnityNull() && bottomNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, terrain.topNeighbor, terrain.rightNeighbor, bottomNeighbor);
            bottomNeighbor.SetNeighbors(bottomNeighbor.leftNeighbor, terrain, bottomNeighbor.rightNeighbor, bottomNeighbor.bottomNeighbor);
        }

        Terrain topNeighbor = Map.Instance[ coord + new float2(0, +1) ]?.Terrain;
        if (!topNeighbor.IsUnityNull() && topNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, topNeighbor, terrain.rightNeighbor, terrain.bottomNeighbor);
            topNeighbor.SetNeighbors(topNeighbor.leftNeighbor, topNeighbor.topNeighbor, topNeighbor.rightNeighbor, terrain);
        }
    }
    private static void SetNeighborsAll()
    {
        if (Map.AllChunks() == null)
        {
            return;
        }
        foreach (ChunkObject chunkObject in Map.AllChunks())
        {
            SetNeighbors(chunkObject.Coord);
        }
    }
}
}