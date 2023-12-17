using System;
using UnityEngine;
using Console = UnityEngine.Debug;
using UnityEngine.Serialization;

public class NoiseGenerator : MonoBehaviour
{
	private readonly Perlin _perlin = new();
	/*
	private Terrain _terrain;
	private TerrainCollider _terrainCollider;
	private TerrainData _terrainData;
	*/
	private readonly int width = 257;
	private readonly int depth = 257;
	public int height = 20;
	public float scale = 6.66f;
	public int Seed
	{
		get => Seed;
		set => _perlin.SetSeed(value);
	}
	[Range(1, 10)]
	public int octaves = 8;
	[Range(0, 2)]
	public float persistance = 1.5f;
	[Range(0, 2)]
	public float lacunarity = 0.5f;
	/*
	public void Awake()
	{
		_terrain = gameObject.AddComponent<Terrain>();
		_terrainCollider = gameObject.AddComponent<TerrainCollider>();
		_terrainData = new TerrainData();
		_terrainCollider.terrainData = _terrainData;
		_terrain.terrainData = _terrainData;
	}
	public void Start()
	{
		_terrainData = GenerateTerrain(_terrain.terrainData);
		_terrain.terrainData = _terrainData;
		_terrainCollider.terrainData = _terrainData;
	}
	*/
	
	[Obsolete("Use ChunkGenerator instead")]
	private TerrainData GenerateTerrain(TerrainData terrainData)
	{
		terrainData.heightmapResolution = width;
		terrainData.size = new Vector3(width, height, depth);
		terrainData.SetHeights(0, 0, GenerateNoise());
		return terrainData;
	}
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
		// Normalize noise
		
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
