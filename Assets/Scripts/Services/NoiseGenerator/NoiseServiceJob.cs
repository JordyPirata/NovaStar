using Config;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Services.Interfaces;
using Services.NoiseGenerator;

namespace Services
{
    public class NoiseServiceJob 
	{
		// Generate noise for the chunk
		public float[] GenerateNoise(float2 coords)
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
		public float[,] GenerateNoise(float2 coords, int width, int height)
		{
			var total = width * height;
			// Calculate the initial x and y
			var iCoordX = (int)coords.x * width;
			var iCoordY = (int)coords.y * height;
			// Define the heights and allCoords arrays
			NativeArray<float> heights = new(total, Allocator.TempJob);
			NativeArray<float2> allCoords = new(total, Allocator.TempJob);
			int initialY = iCoordY, i = 0;

			for (var y = 0; y < width; y++)
			{
				for (var x = 0; x < height; x++)
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
			var jobHandle = noiseGeneratorJob.Schedule(total, 65);
			jobHandle.Complete();

			var result = heights.ToArray();
			// Dispose the arrays
			heights.Dispose();
			allCoords.Dispose();
			return Util.TransferData.TransferDataFromArrayTo2DArray(result, width, height);
		}

        public float[,] GenerateNoise(float2 coords, int width, int height, NoiseState state)
        {
            throw new System.NotImplementedException();
        }
    }
}