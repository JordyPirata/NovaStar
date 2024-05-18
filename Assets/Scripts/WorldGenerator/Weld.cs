using Unity.Mathematics;
using UnityEngine;
using Generator;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Weld
{
    public static void SetNeighbors(ChunkGrid<ChunkObject> chunkGrid, float2 coord)
    {
        // Set the neighbors of the chunk
        ChunkObject thisChunk = chunkGrid[coord];
        Terrain terrain = thisChunk.Terrain;

        Terrain leftNeighbor = chunkGrid[coord + new float2(-1, 0)]?.Terrain;
        if (leftNeighbor != null && leftNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(leftNeighbor, terrain.topNeighbor, terrain.rightNeighbor, terrain.bottomNeighbor);
            leftNeighbor.SetNeighbors(leftNeighbor.leftNeighbor, leftNeighbor.topNeighbor, terrain, leftNeighbor.bottomNeighbor);
        }
        
        Terrain rightNeighbor = chunkGrid[ coord + new float2(+1, 0) ]?.Terrain;
        if (rightNeighbor!=null && rightNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, terrain.topNeighbor, rightNeighbor, terrain.bottomNeighbor);
            rightNeighbor.SetNeighbors(terrain, rightNeighbor.topNeighbor, rightNeighbor.rightNeighbor, rightNeighbor.bottomNeighbor);
        }

        Terrain bottomNeighbor = chunkGrid[ coord + new float2(0, -1) ]?.Terrain;
        if (bottomNeighbor!=null && bottomNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, terrain.topNeighbor, terrain.rightNeighbor, bottomNeighbor);
            bottomNeighbor.SetNeighbors(bottomNeighbor.leftNeighbor, terrain, bottomNeighbor.rightNeighbor, bottomNeighbor.bottomNeighbor);
        }

        Terrain topNeighbor = chunkGrid[ coord + new float2(0, +1) ]?.Terrain;
        if (topNeighbor!=null && topNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, topNeighbor, terrain.rightNeighbor, terrain.bottomNeighbor);
            topNeighbor.SetNeighbors(topNeighbor.leftNeighbor, topNeighbor.topNeighbor, topNeighbor.rightNeighbor, terrain);
        }
    }
    public static void SetNeighborsAll (ChunkGrid<ChunkObject> chunkGrid)
    {
        Stopwatch timer = null;
        timer = new Stopwatch();
        timer.Start();
        if (chunkGrid.AllChunks() == null)
        {
            Debug.LogError("ChunkGrid is null");
            return;
        }
        
        foreach (ChunkObject chunkObject in chunkGrid.AllChunks())
        {
            SetNeighbors(chunkGrid, chunkObject.Coord);
            Debug.Log("Set neighbors for " + chunkObject.Coord);
        }
        timer.Stop();
        Debug.Log("Neighboring in " + timer.Elapsed.TotalMilliseconds + "ms" + " (" + timer.ElapsedMilliseconds + ")");
    }
}