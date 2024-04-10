// Create factory
using Unity.Jobs;
using Unity.Collections;
using Generator;

public struct ChunkGeneratorJob : IJobParallelFor
{
    NativeArray<Chunk> chunks;
    public void Execute(int index)
    {
        throw new System.NotImplementedException();

    }
}