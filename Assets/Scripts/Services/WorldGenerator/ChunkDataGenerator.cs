using System.IO;
using System.Threading.Tasks;
using Config;
using Models;
using Services.Interfaces;
using Util;
using Unity.Mathematics;
using UnityEngine;
using Services.NoiseGenerator;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Services.WorldGenerator
{
/// <summary>
///  This struct is responsible for generating the chunk data
/// </summary>
public struct ChunkDataGenerator
{
    static string message;
    private static IRepository GameRepository => ServiceLocator.GetService<IRepository>();
    private static string WorldDirectory => ServiceLocator.GetService<IWorldData>().GetDirectory();
    private static INoiseDirector NoiseDirector => ServiceLocator.GetService<INoiseDirector>();
    private static IWorldData WorldData => ServiceLocator.GetService<IWorldData>();

    public static async Task<Chunk> Generate(float2 coord)
    {
        string chunkName = $"Chunk({coord.x},{coord.y})";
        string chunkPath = Path.Combine(WorldDirectory, string.Concat(chunkName, ".bin"));
        Chunk chunk;
        // Check if the chunk exists
        if (GameRepository.ExistsFile(chunkPath))
        {
            // Load the chunk
            (message, chunk) = await GameRepository.ReadAsync<Chunk>(chunkPath);

            chunk.position = new float3(coord.x * ChunkConfig.width, 0, coord.y * ChunkConfig.depth);
            Debug.Log(message + " " + chunk.ChunkName);
            return chunk;
        }
        // Create the chunk
        chunk = new()
        {
            position = new float3(coord.x * ChunkConfig.width, 0, coord.y * ChunkConfig.depth),
            ChunkName = chunkName,
            width = ChunkConfig.width,
            depth = ChunkConfig.depth,
            height = ChunkConfig.Height,
            heights = GenerateHighMap(coord),
            temperatures = GenerateTemperatureMap(coord),
            humidity = GenerateHumidityMap(coord)
        };
        // Add the chunk to the list
        SaveChunk(chunk);
        return chunk;
        
    }
    private static float[] GenerateHighMap(float2 coord)
    {
        NoiseDirector.SetBuilder(new ChunkNoiseBuilder());
        return NoiseDirector.GetNoise(coord) as float[];
    }
    private static float[] GenerateTemperatureMap(float2 coord)
    {
        NoiseDirector.SetBuilder(new TempChunkNoiseBuilder());
        (float TAmp, float Tdist) = NoiseRange.GetTemperatureAmpDist(WorldData.GetTemperatureRange());
        NoiseDirector.SetExternalState(
            new NoiseState
            {
                seed = WorldData.GetSeed() - 1,
                frequency = 0.0001f,
                octaves = 1,
                amplitude = TAmp,
                distance = Tdist,
            });
        return NoiseDirector.GetNoise(coord) as float[];
    }
    private static float[] GenerateHumidityMap(float2 coord)
    {
        NoiseDirector.SetBuilder(new HumidityChunkNoiseBuilder());
        (float HAmp, float Hdist) = NoiseRange.GetHumidityAmpDist(WorldData.GetHumidityRange());
        NoiseDirector.SetExternalState(
            new NoiseState
            {
                seed = WorldData.GetSeed() + 1,
                frequency = 0.0001f,
                octaves = 1,
                amplitude = HAmp,
                distance = Hdist,
            });
        return NoiseDirector.GetNoise(coord) as float[];
    }
    private static async void SaveChunk(Chunk Chunk)
    {
        message = await GameRepository.CreateAsync(Chunk,
            Path.Combine(WorldDirectory, string.Concat(Chunk.ChunkName, ".bin")));
        Debug.Log(message + " " + Chunk.ChunkName);
    }

}
}