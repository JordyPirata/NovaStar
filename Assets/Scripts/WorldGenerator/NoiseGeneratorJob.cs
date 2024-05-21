using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

public struct NoiseGeneratorJob : IJobParallelFor
{
    public NativeArray<float2> AllCoords;
    public NativeArray<float> Heights;
    public void Execute(int index)
    {
        Heights[index] = noise.cnoise(AllCoords[index] * ChunkManager.offset) * 0.5f + 0.5f;
    }

}