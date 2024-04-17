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
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < depth; j++)
                {
                    newArray[i, j] = array[(i * width) + j];
                }
            }
            return newArray;
        }
        public static Chunk TrasferDataToChunk(UnManagedChunk unManagedChunk, NativeArray<float> heights, NativeArray<float> temperatures, NativeArray<float> moisture)
        {
            Chunk chunk = new()
            {
                position = unManagedChunk.position,
                ChunkName = unManagedChunk.name.ToString(),
                width = unManagedChunk.width,
                depth = unManagedChunk.depth,
                height = unManagedChunk.height,
                CoordX = unManagedChunk.CoordX,
                CoordY = unManagedChunk.CoordY,
                IsLoaded = unManagedChunk.IsLoaded,
                heights = heights.ToArray(),
                temperatures = heights.ToArray(),
                moisture = heights.ToArray(),
            };
            heights.Dispose();
            temperatures.Dispose();
            moisture.Dispose();

            return chunk;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public static (UnManagedChunk, Arrays) TransferDataToUnManagedChunk(Chunk chunk)
        {
            UnManagedChunk unManagedChunk = new()
            {
                position = chunk.position,
                name = chunk.ChunkName,
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