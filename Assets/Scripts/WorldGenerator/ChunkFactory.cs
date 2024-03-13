// Create factory
using UnityEngine;
using Generator;

public class ChunkFactory
{
    public static Chunk CreateChunk(string name)
    {
        int arraySize = ChunkManager.width * ChunkManager.depth;
        return new Chunk()
        {
            ChunkName = name,
            width = ChunkManager.width,
            depth = ChunkManager.depth,
            height = ChunkManager.height,
            position = new Vector2(0, 0),
            CoordX = 0,
            CoordY = 0,
            IsLoaded = false,
            heights = new float[arraySize],
            temperatures = new float[arraySize],
            moisture = new float[arraySize]
        };
    }
}
