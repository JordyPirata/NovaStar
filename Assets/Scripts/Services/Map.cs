using System.Collections.Generic;
using Unity.Mathematics;
using Services.Interfaces;
using Services.WorldGenerator;
using System;

namespace Services
{
    public class Map<ChunkObject> : IMap<ChunkObject>, IDisposable
    {

        private static readonly object _lock = new();
        private static readonly Dictionary<float2, ChunkObject> grid = new();
        public IEnumerable<ChunkObject> AllChunks ()
		{
            // Lock the grid to prevent concurrent access
            lock (_lock)
            {
                foreach (KeyValuePair<float2, ChunkObject> kvp in grid)
                    yield return kvp.Value;
            }
		}
        public ChunkObject this[float2 coord]
        {
            get => grid.TryGetValue(coord, out ChunkObject chunk) ? chunk : default;
            set
            {
                grid[coord] = value;
            }
        }
        public ChunkObject this[float x, float y]
        {
            get => grid.TryGetValue(new float2(x, y), out ChunkObject chunk) ? chunk : default;
            set
            {
                grid[new float2(x, y)] = value;
            }
        }
        public bool ContainsKey(float2 coord)
        {
            return grid.ContainsKey(coord); 
        }
        public void Add(float2 coord, ChunkObject chunk)
        {
            
            grid.Add(coord, chunk);
            
        }
        public void Remove(float2 coord)
        {
            grid.Remove(coord);
        }

        public void Dispose()
        {
            grid.Clear();
        }
    }
}