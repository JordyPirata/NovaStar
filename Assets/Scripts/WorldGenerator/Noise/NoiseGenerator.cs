using System;
using Unity.Mathematics;
using UnityEngine;
using Repository;
using System.Threading.Tasks;

namespace Generator
{
	public struct  NoiseGenerator
	{
		// TODO:  Move variables to ChunkManager
		private const int octaves = 8;
		public const float persistance = Mathf.PI / 2;
		public const float lacunarity = .5f;
		public static float[] GenerateNoise(int coordX, int coordY)
		{
			float[] heigths = new float[ChunkManager.width * ChunkManager.depth];
			int initialY = coordY, i = 0;

			for (int x = 0; x < ChunkManager.width; x++)
			{
				// Itera CoordX en uno veces width
				for (int y = 0; y < ChunkManager.depth; y++)
				{
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