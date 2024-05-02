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

			int initialX = iCoordX, i = 0;

			for (int y = 0; y < ChunkManager.width; y++)
			{
				for (int x = 0; x < ChunkManager.depth; x++)
				{
					// Calculate the actual x and y
					allCoords[i] = new int2(iCoordX , iCoordY);
					// is negative
					iCoordX++;
					i++;
				}
				iCoordY++;
				// Reset the y
				iCoordX = initialX;
			}
			// Generate the noise

			NoiseGeneratorJob noiseGeneratorJob = new()
            {
				AllCoords = allCoords,
				Heights = heights,
			};
			JobHandle jobHandle = noiseGeneratorJob.Schedule(ChunkManager.length, 32);
			jobHandle.Complete();
			
			var result = heights.ToArray();
			// Dispose the arrays
			heights.Dispose();
			allCoords.Dispose();
			return result;

		}

	}
}