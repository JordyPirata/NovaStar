using System;
using System.Threading.Tasks;
using Models;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Services.WorldGenerator
{
[BurstCompile]
public struct ChunkBuilder
{
    public static async Task<ChunkObject> GenerateChunk(float2 chunkCoords)
    {
        Chunk chunkData = await ChunkDataGenerator.Generate(chunkCoords);
        var poolItem = ChunkPool.Instance.GetChunk(chunkCoords) ?? throw new Exception("chunk pool is full");

        poolItem.GameObject = SetAttributes(poolItem.GameObject, chunkData);
        return poolItem;
    }

    //set position of the chunks
    private static GameObject SetAttributes(GameObject ChunkGameObject, Chunk Chunk)
    {
        if (ChunkGameObject == null) return null;
        ChunkGameObject.transform.position = Chunk.position;
        ChunkGameObject.name = Chunk.ChunkName;
        ChunkGameObject.layer = LayerMask.NameToLayer("Terrain");

        Terrain terrain = ChunkGameObject.GetComponent<Terrain>();
        TerrainCollider terrainCollider = ChunkGameObject.GetComponent<TerrainCollider>();

        terrain = TerrainSettings.ApplySettings(terrain, Chunk);
        terrainCollider.includeLayers = LayerMask.GetMask("Player");
        terrainCollider.terrainData = terrain.terrainData;
        return ChunkGameObject;
    }
}
}
