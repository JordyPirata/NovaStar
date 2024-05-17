using System.Collections.Generic;
using Unity.Mathematics;

namespace Generator
{
    public class ChunkGrid<T> where T : ChunkObject
    {
        public Dictionary<float2, T> grid = new();
        public T this[float2 coord]
        {
            get
            {
                if (grid.TryGetValue(coord, out T t)) return t;
                else return default;
            }
        }
    }

}
