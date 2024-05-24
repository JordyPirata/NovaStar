using Generator;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

public struct NoiseGeneratorJob : IJobParallelFor
{
    public NativeArray<float2> AllCoords;
    public NativeArray<float> Heights;
    public void Execute(int index)
    {
        float total;
        total = noise.cnoise((AllCoords[index] * 0.008f) - 35000) * 0.5f + 0.5f;
        total += noise.cnoise((AllCoords[index] * 0.03f) + 10000) * 0.5f + 0.5f;
        total /= 2;
        Heights[index] = total;
    }
}