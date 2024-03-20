// Create factory
using UnityEngine;
using Generator;

public class ChunkFactory
{
    public ChunkFactory Instance;
    public static Chunk CreateChunk(int coordX, int coordY)
    {
        int arraySize = ChunkManager.width * ChunkManager.depth;
        return new Chunk()
        {
            ChunkName = $"Chunk({coordX},{coordY})",
            width = ChunkManager.width,
            depth = ChunkManager.depth,
            height = ChunkManager.height,
            position = new Vector2(0, 0),
            CoordX = coordX,
            CoordY = coordY,
            IsLoaded = false,
            heights = new float[arraySize],
            temperatures = new float[arraySize],
            moisture = new float[arraySize]
        };
    }
}
