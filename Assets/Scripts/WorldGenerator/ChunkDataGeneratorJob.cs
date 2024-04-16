
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Util;
/// <summary>
///  This job is responsible for generating the chunk data
/// </summary>
public struct ChunkDataGeneratorJob : IJobParallelFor
{
    public NativeArray<int2> chunksCoords;
    public NativeArray<UnManagedChunk> chunks;
    public void Execute(int index)
    {
        chunks[index] = TransferData.TransferDataToUnManagedChunk(ChunkFactory.CreateChunk(chunksCoords[index].x,chunksCoords[index].y));
    }
    
}
