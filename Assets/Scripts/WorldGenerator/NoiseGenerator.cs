using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Generator
{
	public class NoiseGenerator : MonoBehaviour
	{
		// TODO:  Move variables to ChunkManager
		private readonly Perlin _perlin = new();
		private readonly int width = 257;
		private readonly int depth = 257;
		public int height = 20;
		public float scale = 6.66f;
		public int Seed
		{
			get => Seed;
			set => _perlin.SetSeed(value);
		}
		public int octaves = 8;
		public float persistance = Mathf.PI / 2;
		public float lacunarity = .5f;

		public float[,] GenerateNoise()
		{
			float[,] heigths = new float[width, depth];
			float min = 0, max = 0;

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < depth; y++)
				{
					heigths[x, y] = CalculateNoise(x, y);
					if (x == 0 && y == 0)
					{
						min = heigths[x, y];
						max = heigths[x, y];
					}
					else if (heigths[x, y] < min)
					{
						min = heigths[x, y];
					}
					else if (heigths[x, y] > max)
					{
						max = heigths[x, y];
					}
				}
			}

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < depth; y++)
				{
					// InverseLerp returns a value between 0 and 1
					heigths[x, y] = Mathf.InverseLerp(min, max, heigths[x, y]);
				}
			}
			return heigths;
		}

		private float CalculateNoise(int x, int y)
		{
			float xCoord = (float)x / width * scale;
			float yCoord = (float)y / width * scale;
			return _perlin.OctavePerlin(xCoord, yCoord, octaves, persistance, lacunarity);
		}
	}
}