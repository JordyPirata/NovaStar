using Unity.Collections.LowLevel.Unsafe;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Repository;
using Unity.Mathematics;
using Generator;
using System.Linq;
using System;

public class ChunkGenerator 
{
    string message;
    private static ChunkGenerator instance;
    public static ChunkGenerator Instance
    {
        get
        {
            instance ??= new ChunkGenerator();
            return instance;
        }
    }

    public List<GameObject> GenerateChunk(List<GameObject> chunks, float2[] chunksCoords)
    {
        List<Chunk> chunksData = ChunkDataGenerator.Generate(chunksCoords);
        for (int i = 0; i < chunksCoords.Count(); i++)
        {
            chunks[i] = SetAttributes(chunks[i], chunksData[i]);
            chunks[i].SetActive(true);
        }
        return chunks;
    }

    //set position of the chunks
    private static GameObject SetAttributes(GameObject ChunkGameObject, Chunk Chunk)
    {
        ChunkGameObject.transform.position = Chunk.position;
        ChunkGameObject.name = Chunk.ChunkName;
        
        Terrain terrain = ChunkGameObject.GetComponent<Terrain>();
        TerrainCollider terrainCollider = ChunkGameObject.GetComponent<TerrainCollider>();
        
        terrain = TerrainSettings.ApplySettings(terrain, Chunk);
        terrainCollider.terrainData = terrain.terrainData;

        Instance.SaveChunk(Chunk);

        return ChunkGameObject;
    }
    private async void SaveChunk(Chunk Chunk)
    {
        message = await JsonRepository.Instance.CreateAsync(Chunk, 
            Path.Combine(Application.persistentDataPath, string.Concat(Chunk.ChunkName, ".json")));
        Debug.Log(message);
    }
}
