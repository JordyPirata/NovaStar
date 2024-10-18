using System.IO;
using System.Threading.Tasks;
using Config;
using Models;
using Services.Interfaces;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using Services.NoiseGenerator;

namespace Services.WorldGenerator
{
/// <summary>
///  This struct is responsible for generating the chunk data
/// </summary>
[BurstCompile]
public struct ChunkDataGenerator
{

    static string message;
    private static IRepository GameRepository => ServiceLocator.GetService<IRepository>();
    private static string WorldDirectory => ServiceLocator.GetService<IWorldData>().GetDirectory();
    private static INoiseDirector NoiseDirector => ServiceLocator.GetService<INoiseDirector>();

    public static async Task<Chunk> Generate(float2 coord)
    {
        string chunkName = $"Chunk({coord.x},{coord.y})";
        string chunkPath = Path.Combine(WorldDirectory, string.Concat(chunkName, ".bin"));
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
            NoiseDirector.SetBuilder(new ChunkNoiseBuilder());

            // Create the chunk
            chunk = new()
            {
                position = new float3(coord.x * ChunkConfig.width, 0, coord.y * ChunkConfig.depth),
                ChunkName = chunkName,
                width = ChunkConfig.width,
                depth = ChunkConfig.depth,
                height = ChunkConfig.Height,
                heights = NoiseDirector.MakeNoise(coord) as float[],
            };
            // Add the chunk to the list
            SaveChunk(chunk);
            return chunk;
        }
    }
    private static async void SaveChunk(Chunk Chunk)
    {
        message = await GameRepository.Create(Chunk,
            Path.Combine(WorldDirectory, string.Concat(Chunk.ChunkName, ".bin")));
        Debug.Log(message + " " + Chunk.ChunkName);
    }

}
}