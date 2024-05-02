
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Util;
using Generator;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
///  This struct is responsible for generating the chunk data
/// </summary>
public struct ChunkDataGenerator
{

    // TODO: separate the noise generation from the chunk generation
    public static List<Chunk> Generate(int2[] chunkCoords)
    {
        List<Chunk> chunks = new();
        foreach (var coord in chunkCoords)
        {
            // Create the chunk
            Chunk chunk = new()
            {
                position = new Vector3(coord.x * ChunkManager.width, 0, coord.y * ChunkManager.depth),
                ChunkName = $"Chunk({coord.x},{coord.y})",
                width = ChunkManager.width,
                depth = ChunkManager.depth,
                height = ChunkManager.height,
                CoordX = coord.x,
                CoordY = coord.y,
                IsLoaded = false,
                heights = NoiseGenerator.GenerateNoise(coord.x, coord.y),
            };
            // Add the chunk to the list
            chunks.Add(chunk);
        }
        return chunks;

    }
}
