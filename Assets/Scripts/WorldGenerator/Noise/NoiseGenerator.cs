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
			// Define the heights and allCoords arrays
			NativeArray<float> heights = new(ChunkManager.length, Allocator.TempJob);
			NativeArray<int2> allCoords = new(ChunkManager.length, Allocator.TempJob);

			int initialY = coordY, i = 0;
			int actualX, actualY;

			for (int x = 0; x < ChunkManager.width; x++)
			{
				for (int y = 0; y < ChunkManager.depth; y++)
				{
					// Calculate the actual x and y
					allCoords[i] = new int2(coordX , coordY);
					coordY++;
					i++;
				}
				coordX++;
				coordY = initialY;
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

			heights.Dispose();
			allCoords.Dispose();
			return result;

		}

	}
}