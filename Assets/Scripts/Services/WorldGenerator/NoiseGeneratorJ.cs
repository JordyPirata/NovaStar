using Config;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Services.WorldGenerator
{
	[BurstCompile]
	public struct NoiseGeneratorJ
	{
		// Generate noise for the chunk
		public static float[] UseJobs(float2 coords)
		{
			// Calculate the initial x and y
			var iCoordX = (int)coords.x * ChunkConfig.width - (int)coords.x;
			var iCoordY = (int)coords.y * ChunkConfig.depth - (int)coords.y;
			// Define the heights and allCoords arrays
			NativeArray<float> heights = new(ChunkConfig.Length, Allocator.TempJob);
			NativeArray<float2> allCoords = new(ChunkConfig.Length, Allocator.TempJob);

			int initialY = iCoordY, i = 0;

			for (var y = 0; y < ChunkConfig.width; y++)
			{
				for (var x = 0; x < ChunkConfig.depth; x++)
				{
					// Calculate the actual x and y
					allCoords[i] = new int2(iCoordX, iCoordY);
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
				Heights = heights,
			};
			var jobHandle = noiseGeneratorJob.Schedule(ChunkConfig.Length, 65);
			jobHandle.Complete();

			var result = heights.ToArray();
			// Dispose the arrays
			heights.Dispose();
			allCoords.Dispose();
			return result;
		}
	}
}