using UnityEngine;
using Unity.Mathematics;
using Generator;

public class ChunkGenerator 
{
    public static async void GenerateChunk(GameObject[] chunks, float2[] chunksCoords)
    {
        int i = 0;
        await foreach (var chunksData in ChunkDataGenerator.Instance.Generate(chunksCoords))
        {
            chunks[i] = SetAttributes(chunks[i], chunksData);
            chunks[i].SetActive(true);
            i++;
        }
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
