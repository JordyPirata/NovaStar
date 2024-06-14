using Unity.Mathematics;
using UnityEngine;
using System.IO;
using Unity.Burst;
using System.Threading.Tasks;
using Services;

namespace WorldGenerator
{
/// <summary>
///  This struct is responsible for generating the chunk data
/// </summary>
[BurstCompile]
public struct ChunkDataGenerator
{

    static string message;
    private static IRepository GameRepository => ServiceLocator.GetService<IRepository>();

    public static async Task<Chunk> Generate(float2 coord)
    {
        
        string chunkName = $"Chunk({coord.x},{coord.y})";
        string chunkPath = Path.Combine(Application.persistentDataPath, string.Concat(chunkName, ".bin"));
        Chunk chunk;
        // Check if the chunk exists
        if (GameRepository.ExistsFile(chunkPath))
        {
            // Load the chunk
            (message, chunk) = await GameRepository.Read<Chunk>(chunkPath);

            chunk.position = new float3(coord.x * ChunkConfig.width, 0, coord.y * ChunkConfig.depth);
            Debug.Log(message + " " + chunk.ChunkName);
            // Add the chunk to the list
            return chunk;
        }
        else
        {
            // Create the chunk
            chunk = new()
            {
                position = new float3(coord.x * ChunkConfig.width, 0, coord.y * ChunkConfig.depth),
                ChunkName = chunkName,
                width = ChunkConfig.width,
                depth = ChunkConfig.depth,
                height = ChunkConfig.height,
                heights = NoiseGeneratorS.Instance.GenerateNoise(coord),
            };
            // Add the chunk to the list
            SaveChunk(chunk);
            return chunk;
        }
    }
    private static async void SaveChunk(Chunk Chunk)
    {
        message = await GameRepository.Create(Chunk,
            Path.Combine(Application.persistentDataPath, string.Concat(Chunk.ChunkName, ".bin")));
        Debug.Log(message + " " + Chunk.ChunkName);
    }

}
}