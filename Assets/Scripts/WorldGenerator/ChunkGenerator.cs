using UnityEngine;
using Unity.Mathematics;
using Generator;
using System.Threading.Tasks;
using System;

public class ChunkGenerator 
{
    public static PoolItem GenerateChunk(float2 chunkCoords)
    {
        var chunkData = ChunkDataGenerator.Instance.Generate(chunkCoords);
        var poolItem = ChunkPool.Instance.GetChunk(chunkCoords) ?? throw new Exception("chunk pool is full");

        poolItem.GameObject = SetAttributes(poolItem!.GameObject, chunkData);
        return poolItem;

    }

    //set position of the chunks
    private static GameObject SetAttributes(GameObject ChunkGameObject, Chunk Chunk)
    {
        ChunkGameObject.transform.position = Chunk.position;
        ChunkGameObject.name = Chunk.ChunkName;
        ChunkGameObject.layer = LayerMask.NameToLayer("Default");
        
        Terrain terrain = ChunkGameObject.GetComponent<Terrain>();
        TerrainCollider terrainCollider = ChunkGameObject.GetComponent<TerrainCollider>();
        
        terrain = TerrainSettings.ApplySettings(terrain, Chunk);
        terrainCollider.terrainData = terrain.terrainData;
        return ChunkGameObject;
    }

}
