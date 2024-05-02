// Purpose: Contains the TransferData struct, which contains a method to transfer a 1D array to a 2D array.
using System;
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
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < depth; x++)
                {
                    newArray[x, y] = array[(y * width) + x];
                }
            }
            return newArray;
        }
        public static Chunk TrasferDataToChunk(UnManagedChunk unManagedChunk, float[] heights)
        {
            Chunk chunk = new()
            {
                position = unManagedChunk.position,
                ChunkName = unManagedChunk.ChunkName.ToString(),
                width = unManagedChunk.width,
                depth = unManagedChunk.depth,
                height = unManagedChunk.height,
                CoordX = unManagedChunk.CoordX,
                CoordY = unManagedChunk.CoordY,
                IsLoaded = unManagedChunk.IsLoaded,
                heights = heights,
                temperatures = null,
                moisture = null,
            };
            
            return chunk;
        }
        public static float[] TransferDataFromMasterArrayToChunkArray(float[] masterArray, int index, int length)
        {
            float[] chunkArray = new float[length];
            for (int i = 0; i < length; i++)
            {
                chunkArray[i] = masterArray[(index * length) + i];
            }
            return chunkArray;
        }

        [Obsolete("This method is obsolete")]
        public static (UnManagedChunk, Arrays) TransferDataToUnManagedChunk(Chunk chunk)
        {
            UnManagedChunk unManagedChunk = new()
            {
                position = chunk.position,
                ChunkName = chunk.ChunkName,
                width = chunk.width,
                depth = chunk.depth,
                height = chunk.height,
                CoordX = chunk.CoordX,
                CoordY = chunk.CoordY,
                IsLoaded = chunk.IsLoaded, 
            };
            Arrays arrays = new()
            {
                heights = new NativeArray<float>(chunk.heights, Allocator.TempJob),
                temperatures = new NativeArray<float>(chunk.temperatures, Allocator.TempJob),
                moisture = new NativeArray<float>(chunk.moisture, Allocator.TempJob)
            };
            arrays.Inicialize();
            return (unManagedChunk, arrays);
        }
    }
}