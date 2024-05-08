using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Generator;
using System;
using UnityEngine;

public struct NoiseGeneratorJob : IJobParallelFor
{
    public NativeArray<float2> AllCoords;
    public NativeArray<float> Heights;
    // TODO: Assing the constants variables to offset
    public void Execute(int index)
    {
        Heights[index] = noise.cnoise(AllCoords[index] * ChunkManager.offset) * 0.5f + 0.5f;
    }

    public void SPerlin(int index)
    {
        Heights[index] = Perlin.CalculatePerlin(AllCoords[index].x * ChunkManager.offset, AllCoords[index].y * ChunkManager.offset , ChunkManager.Instance.Permutation);
        Heights[index] /= 1.75f;
    }
    public void OPerlin(int index)
    {
        Heights[index] = (Perlin.OctavePerlin(AllCoords[index].x, AllCoords[index].y,
            ChunkManager.octaves, ChunkManager.persistance, ChunkManager.lacunarity,
            ChunkManager.Instance.Permutation) + 20f) * 0.0175f;
    }

}