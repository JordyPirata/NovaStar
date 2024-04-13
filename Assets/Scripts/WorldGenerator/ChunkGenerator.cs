using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Repository;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;
using Generator;

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

    public static List<GameObject> GenerateChunk(List<GameObject> chunks, List<int2> chunksCoords)
    {
        NativeArray<int2> NativeChunksCoords = new(chunksCoords.ToArray(), Allocator.TempJob);
        NativeArray<Chunk> NativeChunks = new(chunksCoords.Count, Allocator.TempJob);

        ChunkDataGeneratorJob chunkDataGeneratorJob = new()
        {
            chunksCoords = NativeChunksCoords,
            chunks = NativeChunks
        };

        JobHandle jobHandle = chunkDataGeneratorJob.Schedule(chunksCoords.Count, 1);
        jobHandle.Complete();
        
        for (int i = 0; i < chunks.Count; i++)
        {
            chunks[i] = SetAttributes(chunks[i], NativeChunks[i]);
            chunks[i].SetActive(true);
        }

        NativeChunksCoords.Dispose();
        NativeChunks.Dispose();

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

        Instance.SaveChunk(ChunkGameObject);

        return ChunkGameObject;
    }
    private async void SaveChunk(GameObject Chunk)
    {
        message = await JsonRepository.Instance.CreateAsync(Chunk, Path.Combine(Application.persistentDataPath, string.Concat(Chunk.name, ".json")));
        Debug.Log(message);
    }
}
