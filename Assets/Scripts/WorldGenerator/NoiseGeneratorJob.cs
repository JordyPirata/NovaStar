using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Generator;

public struct NoiseGeneratorJob : IJobParallelFor
{
    public NativeArray<int2> allCoords;
    public NativeArray<float> allHeights;
    public void Execute(int index)
    {
        allHeights[index] = (Perlin.OctavePerlin(allCoords[index].x, allCoords[index].y, 
            ChunkManager.octaves, ChunkManager.persistance, ChunkManager.lacunarity, ChunkManager.Instance.Permutation) + 20f) * 0.0175f;
        throw new System.NotImplementedException();
    }

}