using System;
using Unity.Mathematics;
using UnityEngine;

namespace Generator
{
	public class NoiseGenerator 
	{
		// TODO:  Move variables to ChunkManager
		private readonly Perlin _perlin;
		private readonly int Width = ChunkManager.width;
		private readonly int Depth = ChunkManager.depth;
		private int[] Permutation
		{
			get => ChunkManager.Instance.Permutation().Result;
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
					heigths[i] = (_perlin.OctavePerlin(coordX, coordY, octaves, persistance, lacunarity, Permutation) + 20f) * 0.0175f;
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