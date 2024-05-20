using Unity.Mathematics;
using UnityEngine;
using Generator;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Map = Generator.ChunkGrid<Generator.ChunkObject>;

public class Weld
{
    public static void SetNeighbors(float2 coord)
    {
        // Set the neighbors of the chunk
        ChunkObject thisChunk = Map.grid[coord];
        Terrain terrain = thisChunk.Terrain;

        Terrain leftNeighbor = Map.grid[coord + new float2(-1, 0)]?.Terrain;
        if (leftNeighbor != null && leftNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(leftNeighbor, terrain.topNeighbor, terrain.rightNeighbor, terrain.bottomNeighbor);
            leftNeighbor.SetNeighbors(leftNeighbor.leftNeighbor, leftNeighbor.topNeighbor, terrain, leftNeighbor.bottomNeighbor);
        }
        
        Terrain rightNeighbor = Map.grid[ coord + new float2(+1, 0) ]?.Terrain;
        if (rightNeighbor!=null && rightNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, terrain.topNeighbor, rightNeighbor, terrain.bottomNeighbor);
            rightNeighbor.SetNeighbors(terrain, rightNeighbor.topNeighbor, rightNeighbor.rightNeighbor, rightNeighbor.bottomNeighbor);
        }

        Terrain bottomNeighbor = Map.grid[ coord + new float2(0, -1) ]?.Terrain;
        if (bottomNeighbor!=null && bottomNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, terrain.topNeighbor, terrain.rightNeighbor, bottomNeighbor);
            bottomNeighbor.SetNeighbors(bottomNeighbor.leftNeighbor, terrain, bottomNeighbor.rightNeighbor, bottomNeighbor.bottomNeighbor);
        }

        Terrain topNeighbor = Map.grid[ coord + new float2(0, +1) ]?.Terrain;
        if (topNeighbor!=null && topNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, topNeighbor, terrain.rightNeighbor, terrain.bottomNeighbor);
            topNeighbor.SetNeighbors(topNeighbor.leftNeighbor, topNeighbor.topNeighbor, topNeighbor.rightNeighbor, terrain);
        }
    }
    public static void SetNeighborsAll()
    {
        Stopwatch timer = null;
        timer = new Stopwatch();
        timer.Start();
        if (Map.AllChunks() == null)
        {
            Debug.LogError("ChunkGrid is null");
            return;
        }
        
        foreach (ChunkObject chunkObject in Map.AllChunks())
        {
            SetNeighbors(chunkObject.Coord);
            Debug.Log("Set neighbors for " + chunkObject.Coord);
        }
        timer.Stop();
        Debug.Log("Neighboring in " + timer.Elapsed.TotalMilliseconds + "ms" + " (" + timer.ElapsedMilliseconds + ")");
    }
}