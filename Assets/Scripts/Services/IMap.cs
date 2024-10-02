using System.Collections.Generic;
using Unity.Mathematics;
using Services.WorldGenerator;

namespace Services.Interfaces
{
    public interface IMap<ChunkObject> 
    {
        public IEnumerable<ChunkObject> AllChunks();
        public ChunkObject this[float2 coord] { get; set; }
        public ChunkObject this[float x, float y] { get; set; }
        public bool ContainsKey(float2 coord);
        public void Add(float2 coord, ChunkObject chunk);
        public void Remove(float2 coord);
    }
}