
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Util;
using Generator;
using UnityEngine;
using System.Collections.Generic;
using Repository;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Threading;
using Unity.Burst;

/// <summary>
///  This struct is responsible for generating the chunk data
/// </summary>
[BurstCompile]
public class ChunkDataGenerator
{
    private static ChunkDataGenerator instance;
    public static ChunkDataGenerator Instance
    {
        get
        {
            instance ??= new ChunkDataGenerator();
            return instance;
        }
    }
    string message;
    public Chunk Generate(float2 coord)
    {

        string chunkName = $"Chunk({coord.x},{coord.y})";
        string chunkPath = Path.Combine(Application.persistentDataPath, string.Concat(chunkName, ".bin"));
        Chunk chunk;
        // Check if the chunk exists
        if (JsonRepository.Exists(chunkPath))
        {
            // Load the chunk
            (message, chunk) = JsonRepository.Instance.Read<Chunk>(chunkPath);

            chunk.position = new float3(coord.x * ChunkManager.width, 0, coord.y * ChunkManager.depth);
            Debug.Log(message + " " + chunk.ChunkName);
            // Add the chunk to the list
            return chunk;
        }
        else
        {
            // Create the chunk
            chunk = new()
            {
                position = new float3(coord.x * ChunkManager.width, 0, coord.y * ChunkManager.depth),
                ChunkName = chunkName,
                width = ChunkManager.width,
                depth = ChunkManager.depth,
                height = ChunkManager.height,
                heights = NoiseGenerator.GenerateNoise(coord),
            };
            // Add the chunk to the list
            SaveChunk(chunk);
            return chunk;
        }

    }
    private void SaveChunk(Chunk Chunk)
    {
        message = JsonRepository.Instance.Create(Chunk,
            Path.Combine(Application.persistentDataPath, string.Concat(Chunk.ChunkName, ".bin")));
        Debug.Log(message + " " + Chunk.ChunkName);
    }

}
