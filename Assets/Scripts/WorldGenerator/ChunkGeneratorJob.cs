// Create factory
using Unity.Jobs;
using Unity.Collections;
using UnityEditor.Profiling.Memory.Experimental;
using Generator;

public struct ChunkGeneratorJob : IJob
{
    NativeArray<Chunk> chunks;
    public void Execute()
    {
        throw new System.NotImplementedException();
    }
}