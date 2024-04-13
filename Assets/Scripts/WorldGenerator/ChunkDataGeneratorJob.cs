// Create factory
using Unity.Jobs;
using Unity.Collections;
using Generator;
using Unity.Mathematics;
using UnityEngine;
using System.IO;
using Repository;
// TODO: test and finish this class or change the Instantiate method to another class
public struct ChunkDataGeneratorJob : IJobParallelFor
{
    string message;
    public NativeArray<int2> chunksCoords;
    public NativeArray<Chunk> chunks;
    public void Execute(int index)
    {
        chunks[index] = ChunkFactory.CreateChunk(chunksCoords[index].x,chunksCoords[index].y);
        
    }
    
}
