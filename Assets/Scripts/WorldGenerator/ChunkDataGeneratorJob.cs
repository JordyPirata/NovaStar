
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Util;
using Generator;
using UnityEngine;

/// <summary>
///  This job is responsible for generating the chunk data
/// </summary>
public struct ChunkDataGeneratorJob : IJobParallelFor
{
    private Arrays arrays;
    public NativeArray<int2> chunksCoords;
    public NativeArray<UnManagedChunk> chunks;
    public NativeSlice<NativeArray<float>> heights;

    public void Execute(int index)
    {
        int coordX = chunksCoords[index].x;
        int coordY = chunksCoords[index].y;
        
        chunks[index] = new() {
            position = new Vector3(coordX * ChunkManager.width, 0, coordY * ChunkManager.depth),
            ChunkName = $"Chunk({coordX},{coordY})",
            width = ChunkManager.width,
            depth = ChunkManager.depth,
            height = ChunkManager.height,
            CoordX = coordX,
            CoordY = coordY,
            IsLoaded = false,
            
        };
        heights[index] = new NativeArray<float>(NoiseGenerator.GenerateNoise(coordX, coordY), Allocator.TempJob);
        
    }
}
