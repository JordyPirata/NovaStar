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
		private int Width { get; set; } = ChunkManager.Instance.width;
		private int Depth { get; set; } = ChunkManager.Instance.depth;
		private float Scale { get; set; } = ChunkManager.Instance.scale;
		public int Seed
		{
			get => ChunkManager.Instance.seed;
			set => _perlin.SetSeed(value);
		}
		public int octaves = 8;
		public const float persistance = Mathf.PI / 2;
		public const float lacunarity = .5f;
		public float[,] GenerateNoise(int coordX, int coordY)
		{
			float[,] heigths = new float[Width, Depth];
			float min = 0, max = 0;

			for (int x = coordX; x < coordX + Width; x++)
			{
				for (int y = coordY; y < coordY + Depth; y++)
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

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Depth; y++)
				{
					// InverseLerp returns a value between 0 and 1
					heigths[x, y] = Mathf.InverseLerp(min, max, heigths[x, y]);
				}
			}
			return heigths;
		}

		private float CalculateNoise(int x, int y)
		{
			float xCoord = (float)x / Width * Scale;
			float yCoord = (float)y / Width * Scale;
			return _perlin.OctavePerlin(xCoord, yCoord, octaves, persistance, lacunarity);
		}
	}
}