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
		public static float[] GenerateNoise(float2 coords)
		{
			int iCoordX = (int)coords.x * ChunkManager.width;
			int iCoordY = (int)coords.y * ChunkManager.depth;
			// Define the heights and allCoords arrays
			NativeArray<float> heights = new(ChunkManager.length, Allocator.TempJob);
			NativeArray<float2> allCoords = new(ChunkManager.length, Allocator.TempJob);

			int initialY = iCoordY, i = 0;

			for (int y = 0; y < ChunkManager.width; y++)
			{
				for (int x = 0; x < ChunkManager.depth; x++)
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
			JobHandle jobHandle = noiseGeneratorJob.Schedule(ChunkManager.length, 33);
			jobHandle.Complete();
			
			var result = heights.ToArray();
			// Dispose the arrays
			heights.Dispose();
			allCoords.Dispose();
			return result;

		}

	}
}