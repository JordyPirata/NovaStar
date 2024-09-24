using Unity.Mathematics;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections;
using Services.Interfaces;
using Services.WorldGenerator;

namespace Services
{
/// <summary>
/// This class is responsible for welding the chunks together
/// </summary>

public class WeldMapService : MonoBehaviour, IWeldMap
{
    private static IMap<ChunkObject> Map;
    public void Awake()
    {
       Map = ServiceLocator.GetService<IMap<ChunkObject>>();
    }
    public void StartService()
    {
        StartCoroutine(WeldChunks());
    }
    public void StopService()
    {
        StopCoroutine(WeldChunks());
    }

    private IEnumerator WeldChunks()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            SetAllNeighbors();
        }
    }
    private static void SetNeighbors(float2 coord)
    {
        // Set the neighbors of the chunk
        ChunkObject thisChunk = Map[coord];
        Terrain terrain = thisChunk.Terrain;

        Terrain leftNeighbor = Map[coord + new float2(-1, 0)]?.Terrain;
        if (!leftNeighbor.IsUnityNull() && leftNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(leftNeighbor, terrain.topNeighbor, terrain.rightNeighbor, terrain.bottomNeighbor);
            leftNeighbor.SetNeighbors(leftNeighbor.leftNeighbor, leftNeighbor.topNeighbor, terrain, leftNeighbor.bottomNeighbor);
        }
        
        Terrain rightNeighbor = Map[ coord + new float2(+1, 0) ]?.Terrain;
        if (!rightNeighbor.IsUnityNull() && rightNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, terrain.topNeighbor, rightNeighbor, terrain.bottomNeighbor);
            rightNeighbor.SetNeighbors(terrain, rightNeighbor.topNeighbor, rightNeighbor.rightNeighbor, rightNeighbor.bottomNeighbor);
        }

        Terrain bottomNeighbor = Map[ coord + new float2(0, -1) ]?.Terrain;
        if (!bottomNeighbor.IsUnityNull() && bottomNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, terrain.topNeighbor, terrain.rightNeighbor, bottomNeighbor);
            bottomNeighbor.SetNeighbors(bottomNeighbor.leftNeighbor, terrain, bottomNeighbor.rightNeighbor, bottomNeighbor.bottomNeighbor);
        }

        Terrain topNeighbor = Map[ coord + new float2(0, +1) ]?.Terrain;
        if (!topNeighbor.IsUnityNull() && topNeighbor.isActiveAndEnabled)
        {
            terrain.SetNeighbors(terrain.leftNeighbor, topNeighbor, terrain.rightNeighbor, terrain.bottomNeighbor);
            topNeighbor.SetNeighbors(topNeighbor.leftNeighbor, topNeighbor.topNeighbor, topNeighbor.rightNeighbor, terrain);
        }
    }
    private static void SetAllNeighbors()
    {
        if (Map.AllChunks() == null)
        {
            return;
        }
        foreach (ChunkObject chunkObject in Map.AllChunks())
        {
            SetNeighbors(chunkObject.Coord);
        }
        Debug.Log("SetNeighbors");
    }
}
}