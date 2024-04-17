
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Util;

/// <summary>
///  This job is responsible for generating the chunk data
/// </summary>
public struct ChunkDataGeneratorJob : IJobParallelFor
{
    private Arrays arrays;
    public NativeArray<int2> chunksCoords;
    public NativeArray<UnManagedChunk> chunks;
    public NativeArray<NativeArray<float>> heights;
    public NativeArray<NativeArray<float>> temperatures;
    public NativeArray<NativeArray<float>> moisture;
    public void Execute(int index)
    {

        arrays = new Arrays();
        (chunks[index], arrays) = TransferData.TransferDataToUnManagedChunk(ChunkFactory.CreateChunk(chunksCoords[index].x,chunksCoords[index].y));
        heights[index] = arrays.heights;
        temperatures[index] = arrays.temperatures;
        moisture[index] = arrays.moisture;
        arrays.Dispose();
    }
}
