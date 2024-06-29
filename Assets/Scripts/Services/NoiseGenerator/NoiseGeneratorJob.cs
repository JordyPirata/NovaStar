using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Services.NoiseGenerator
{

public struct NoiseGeneratorJob : IJobParallelFor
{
    public NativeArray<float2> AllCoords;
    public NativeArray<float> Heights;
    float _total;
    public void Execute(int index)
    {
        _total = noise.cnoise((AllCoords[index] * 0.008f) - 35000) * 0.5f + 0.5f;
        _total += noise.cnoise((AllCoords[index] * 0.03f) + 10000) * 0.5f + 0.5f;
        _total /= 2;
        Heights[index] = _total;
    }
}
}