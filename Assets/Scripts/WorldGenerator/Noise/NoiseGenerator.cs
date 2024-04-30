using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;

namespace Generator
{
	[BurstCompile]
	public struct NoiseGenerator
	{
		// Generate noise for the chunk
		public static float[] GenerateNoise(int coordX, int coordY)
		{
			int iCoordX = coordX * ChunkManager.width;
			int iCoordY = coordY * ChunkManager.depth;
			// Define the heights and allCoords arrays
			NativeArray<float> heights = new(ChunkManager.length, Allocator.TempJob);
			NativeArray<int2> allCoords = new(ChunkManager.length, Allocator.TempJob);

			int initialY = iCoordY, i = 0;

			for (int x = 0; x < ChunkManager.width; x++)
			{
				for (int y = 0; y < ChunkManager.depth; y++)
				{
					// Calculate the actual x and y
					allCoords[i] = new int2(iCoordX , iCoordY);
					// is negative
					iCoordY++;
					i++;
				}
				iCoordX++;
				// Reset the y
				iCoordY = initialY;
			}
			// Generate the noise

			NoiseGeneratorJob noiseGeneratorJob = new()
            {
				AllCoords = allCoords,
				Heights = heights
			};
			JobHandle jobHandle = noiseGeneratorJob.Schedule(ChunkManager.length, 64);
			jobHandle.Complete();
			
			var result = heights.ToArray();
			result[0] = 1f;
			result[1] = 1f;
			result[2] = 1f;

			result[256] = 1f;
			result[257] = 1f;
			result[258] = 1f;

			heights.Dispose();
			allCoords.Dispose();
			return result;

		}

	}
}