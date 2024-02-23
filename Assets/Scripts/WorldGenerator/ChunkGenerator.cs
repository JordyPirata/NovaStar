using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Jobs;
public struct ChunkAccess
{
    public Vector2 position;
    public int CoordX;
    public int CoordY;
    public bool IsLoaded;
    public float[,] heights;
    public float[,] temperatures;
    public float[,] moisture;
}
public struct ChunkGenerator : IJobParallelFor
{
    public NativeArray<ChunkAccess> chunks;
    public void Execute(int index)
    {
        NoiseGenerator noiseGenerator = new();

        ChunkAccess chunk = chunks[index];
        chunk.heights = noiseGenerator.GenerateNoise();
        noiseGenerator.Seed = 1;
        chunk.temperatures = noiseGenerator.GenerateNoise();
        noiseGenerator.Seed = 2;
        chunk.moisture = noiseGenerator.GenerateNoise();

        chunk.CoordX = (int)chunk.position.x;
        chunk.CoordY = (int)chunk.position.y;
        chunks[index] = chunk;

    }
}
