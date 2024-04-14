using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;

public struct UnManagedChunk
{
    public float3 position;
    public FixedString128Bytes name;
    public int width;
    public int depth;
    public int height;
    public int CoordX;
    public int CoordY;
    public bool IsLoaded;
    NativeArray<float> heights;
    NativeArray<float> temperatures;
    NativeArray<float> moisture;

}