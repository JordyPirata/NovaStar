// Create factory
using UnityEngine;
using Generator;
using Unity.Burst;

[BurstCompile]
public class ChunkFactory
{
    public static Chunk CreateChunk(int coordX, int coordY)
    {
        return new Chunk()
        {
            position = new Vector3(coordX * ChunkManager.width, 0, coordY * ChunkManager.depth),
            ChunkName = $"Chunk({coordX},{coordY})",
            width = ChunkManager.width,
            depth = ChunkManager.depth,
            height = ChunkManager.height,
            CoordX = coordX,
            CoordY = coordY,
            IsLoaded = false,
            heights = NoiseGenerator.GenerateNoise(coordX, coordY),
            temperatures = null,
            moisture = null
        };
    }
}
