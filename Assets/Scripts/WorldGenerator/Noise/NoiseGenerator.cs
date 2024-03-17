using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Console = UnityEngine.Debug;

namespace Generator
{
	public class NoiseGenerator 
	{
		// TODO:  Move variables to ChunkManager
		private readonly Perlin _perlin = new();
		private readonly int Width = ChunkManager.width;
		private readonly int Depth = ChunkManager.depth;
		public int Seed
		{
			get => ChunkManager.seed;
			set => _perlin.SetSeed(value);
		}
		private int octaves = 8;
		public const float persistance = Mathf.PI / 2;
		public const float lacunarity = .5f;
		public float[] GenerateNoise(int coordX, int coordY)
		{
			float[] heigths = new float[Width * Depth];
			int initialY = coordY, i = 0;

			for (int x = 0; x < Width; x++)
			{
				// Itera CoordX en uno veces width
				for (int y = 0; y < Depth; y++)
				{
					heigths[i] = (_perlin.OctavePerlin(coordX, coordY, octaves, persistance, lacunarity) + 20f) * 0.0175f;
					coordY ++;
					i ++;
				}
				coordX ++;
				coordY = initialY;
			}
			return heigths;
		}
	}
}