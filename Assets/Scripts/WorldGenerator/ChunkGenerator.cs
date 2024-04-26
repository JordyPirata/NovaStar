using Unity.Collections.LowLevel.Unsafe;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Repository;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;
using Generator;
using Util;
using System.Runtime.InteropServices;

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
        NativeArray<int2> NativeChunksCoords = new(chunksCoords.ToArray(), Allocator.Persistent);
        NativeArray<UnManagedChunk> NativeChunks = new(chunksCoords.Count, Allocator.Persistent); 
        NativeArray<float> AllHeights = new(ChunkManager.length * chunksCoords.Count, Allocator.Persistent);

        ChunkDataGeneratorJob chunkDataGeneratorJob = new()
        {
            chunksCoords = NativeChunksCoords,
            chunks = NativeChunks,
            allHeights = AllHeights,
        };

        JobHandle jobHandle = chunkDataGeneratorJob.Schedule(ChunkManager.length * chunksCoords.Count , ChunkManager.depth);
        jobHandle.Complete();
        
        for (int i = 0; i < chunks.Count; i++)
        {
            chunks[i] = SetAttributes(chunks[i], 
                TransferData.TrasferDataToChunk(chunkDataGeneratorJob.chunks[i], 
                    TransferData.TransferDataFromMasterArrayToChunkArray(chunkDataGeneratorJob.allHeights.ToArray(), i, ChunkManager.length)));
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
