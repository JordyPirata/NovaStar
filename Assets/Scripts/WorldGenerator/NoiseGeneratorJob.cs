using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Generator;

public struct NoiseGeneratorJob : IJobParallelFor
{
    public NativeArray<int2> AllCoords;
    public NativeArray<float> Heights;
    public NativeArray<int> Permutation;
    public void Execute(int index)
    {
        Heights[index] = (Perlin.OctavePerlin(AllCoords[index].x, AllCoords[index].y,
            ChunkManager.octaves, ChunkManager.persistance, ChunkManager.lacunarity,
            Permutation.ToArray()) + 20f) * 0.0175f;
    }

}