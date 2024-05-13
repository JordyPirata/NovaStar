using UnityEngine;
using Unity.Mathematics;
using Generator;
using System.Threading.Tasks;

public class ChunkGenerator 
{
    public static async Task<GameObject> GenerateChunk(float2 chunkCoords)
    {
        var chunkData = await ChunkDataGenerator.Instance.Generate(chunkCoords);
        
        GameObject chunk = SetAttributes(ChunkPool.Instance.GetChunk(chunkCoords), chunkData);
        chunk.SetActive(true);
        return chunk;
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
