using System.Collections.Generic;
using Unity.Mathematics;
// TODO: Lock the grid to prevent concurrent access
namespace Generator
{
    public class ChunkGrid<T> where T : ChunkObject
    {
        public static Dictionary<float2, T> grid = new();
        static public IEnumerable<T> AllChunks ()
		{
			foreach (KeyValuePair<float2,T> kvp in grid)
				yield return kvp.Value;
		}
    }

}
