// Purpose: Contains the TransferData struct, which contains a method to transfer a 1D array to a 2D array.
using Unity.Burst;
using Unity.Collections;

namespace Util
{
    [BurstCompile]
    public struct TransferData
    {
        public static float[,] TransferDataFromArrayTo2DArray(float[] array, int width, int depth)
        {
            float[,] newArray = new float[width, depth];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < depth; j++)
                {
                    newArray[i, j] = array[(i * width) + j];
                }
            }
            return newArray;
        }
        public static Chunk TrasferDataToChunk(UnManagedChunk unManagedChunk)
        {
            return new Chunk()
            {
                position = unManagedChunk.position,
                ChunkName = unManagedChunk.name.ToString(),
                width = unManagedChunk.width,
                depth = unManagedChunk.depth,
                height = unManagedChunk.height,
                CoordX = unManagedChunk.CoordX,
                CoordY = unManagedChunk.CoordY,
                IsLoaded = unManagedChunk.IsLoaded,
                heights = unManagedChunk.heights.ToArray(),
                temperatures = unManagedChunk.temperatures.ToArray(),
                moisture = unManagedChunk.moisture.ToArray(),
            };
        }

        public static UnManagedChunk TransferDataToUnManagedChunk(Chunk chunk)
        {
            return new UnManagedChunk()
            {
                position = chunk.position,
                name = chunk.ChunkName,
                width = chunk.width,
                depth = chunk.depth,
                height = chunk.height,
                CoordX = chunk.CoordX,
                CoordY = chunk.CoordY,
                IsLoaded = chunk.IsLoaded,
                heights = new NativeArray<float>(chunk.heights, Allocator.None),
                temperatures = new NativeArray<float>(chunk.temperatures, Allocator.TempJob),
                moisture = new NativeArray<float>(chunk.moisture, Allocator.TempJob),
            };
        }
    }
}