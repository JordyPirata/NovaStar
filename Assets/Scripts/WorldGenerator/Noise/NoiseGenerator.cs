using UnityEngine;
using Unity.Burst;

namespace Generator
{
	[BurstCompile]
	public struct  NoiseGenerator
	{
		private const int octaves = 8;
		public const float persistance = Mathf.PI / 2;
		public const float lacunarity = .5f;
		// Generate noise for the chunk
		public static float[] GenerateNoise(int coordX, int coordY)
		{
			// Define the heights of the chunk
			float[] heigths = new float[ChunkManager.length];
			int initialY = coordY, i = 0;

			for (int x = 0; x < ChunkManager.width; x++)
			{
				for (int y = 0; y < ChunkManager.depth; y++)
				{
					// Assign the height to the heights array
					heigths[i] = (Perlin.OctavePerlin(coordX, coordY, octaves, persistance, lacunarity, ChunkManager.Instance.Permutation) + 20f) * 0.0175f;
					coordY++;
					i++;
				}
				coordX++;
				coordY = initialY;
			}
			return heigths;
		}

	}
}