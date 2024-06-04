using Unity.Mathematics;
using Generator;
using UnityEngine;
using Repository;
using System.IO;
using Unity.Burst;
using System.Threading.Tasks;

/// <summary>
///  This struct is responsible for generating the chunk data
/// </summary>
[BurstCompile]
public struct ChunkDataGenerator
{

    static string message;
    public static async Task<Chunk> Generate(float2 coord)
    {

        string chunkName = $"Chunk({coord.x},{coord.y})";
        string chunkPath = Path.Combine(Application.persistentDataPath, string.Concat(chunkName, ".bin"));
        Chunk chunk;
        // Check if the chunk exists
        if (GameRepository.Exists(chunkPath))
        {
            // Load the chunk
            (message, chunk) = await GameRepository.Instance.Read<Chunk>(chunkPath);

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
                heights = NoiseGeneratorS.Instance.GenerateNoise(coord),
            };
            // Add the chunk to the list
            SaveChunk(chunk);
            return chunk;
        }
    }
    private static async void SaveChunk(Chunk Chunk)
    {
        message = await GameRepository.Instance.Create(Chunk,
            Path.Combine(Application.persistentDataPath, string.Concat(Chunk.ChunkName, ".bin")));
        Debug.Log(message + " " + Chunk.ChunkName);
    }

}
