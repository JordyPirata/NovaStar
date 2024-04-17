using Unity.Collections;
/// <summary>
/// Need initialize and dispose the arrays
/// </summary>
public struct Arrays
{
    public  NativeArray<float> heights;
    public  NativeArray<float> temperatures;
    public  NativeArray<float> moisture;
    /// <summary>
    /// Initialize the arrays with the length of the ChunkManager
    /// </summary>
    public void Inicialize()
    {
        heights = new NativeArray<float>(ChunkManager.length, Allocator.Persistent);
        temperatures = new NativeArray<float>(ChunkManager.length, Allocator.Persistent);
        moisture = new NativeArray<float>(ChunkManager.length, Allocator.Persistent);
    }
    /// <summary>
    /// Dispose the arrays 
    /// </summary>
    public void Dispose()
    {
        heights.Dispose();
        temperatures.Dispose();
        moisture.Dispose();
    }
}