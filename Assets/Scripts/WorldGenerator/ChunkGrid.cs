using System.Collections.Generic;
using Unity.Mathematics;
// TODO: Lock the grid to prevent concurrent access
namespace Generator
{
    public class ChunkGrid<T> where T : ChunkObject
    {
        private static ChunkGrid<T> instance;
        public static ChunkGrid<T> Instance
        {
            get
            {
                instance ??= new ChunkGrid<T>();
                return instance;
            }
        }
        private static readonly object _lock = new();
        private static readonly Dictionary<float2, T> grid = new();
        public static IEnumerable<T> AllChunks ()
		{
            lock (_lock)
            {
                foreach (KeyValuePair<float2, T> kvp in grid)
                    yield return kvp.Value;
            }
		}
        public T this[float2 coord]
        {
            get
            {
                return grid.TryGetValue(coord, out T chunk) ? chunk : null;
            }
            set
            {
                grid[coord] = value;
            }
        }
        public static bool ContainsKey(float2 coord)
        {
            return grid.ContainsKey(coord); 
        }
        public static void Add(float2 coord, T chunk)
        {
            lock (_lock)
            {
                grid.Add(coord, chunk);
            }
        }
        public static void Remove(float2 coord)
        {
            lock (_lock)
            {
                grid.Remove(coord);
            }
        }
    }
}