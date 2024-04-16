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
    public NativeArray<float> heights;
    public NativeArray<float> temperatures;
    public NativeArray<float> moisture;

    public void Dispose()
    {
        heights.Dispose();
        temperatures.Dispose();
        moisture.Dispose();
    }

}