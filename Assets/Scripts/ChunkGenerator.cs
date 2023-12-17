using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
public struct ChunkAccess
{
    public Chunk chunk;

    public ChunkAccess(Chunk chunk)
    {
        this.chunk = chunk;
    }
}
public struct ChunkGenerator : IJobParallelFor
{
    public NativeArray<ChunkAccess> chunks;
    private Chunk chunk;
    public void Execute(int index)
    {
        NoiseGenerator noiseGenerator = new();

        chunk = chunks[index].chunk;
        chunk.heights = noiseGenerator.GenerateNoise();
        chunk.temperatures = noiseGenerator.GenerateNoise();
        chunk.moisture = noiseGenerator.GenerateNoise();

        ChunkAccess chunkAccess = new(chunk);
        chunks[index] = chunkAccess;
    }
}
