using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TerrainGenerator : MonoBehaviour
{
	private readonly Perlin _perlin = new();
	private Terrain _terrain;
	private TerrainCollider _terrainCollider;
	private TerrainData _terrainData;
	private readonly int width = 255;
	private readonly int depth = 255;
	public int height = 20;
	public float scale = 6f;
	public int seed = 0;
	[Range(1, 10)]
	public int octaves = 8;
	[Range(0, 2)]
	public float persistance = 1.5f;
	[Range(0, 2)]
	public float lacunarity = 0.5f;

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
		_perlin.SetSeed(seed);
		_terrain.terrainData = GenerateTerrain(_terrain.terrainData);
	}
	private TerrainData GenerateTerrain(TerrainData terrainData)
	{
		terrainData.heightmapResolution = width + 1;
		terrainData.size = new Vector3(width, height, depth);
		terrainData.SetHeights(0, 0, GenerateHeights());
		return terrainData;
	}

	private float[,] GenerateHeights()
	{
		float[,] heigths = new float[width, depth];
		float min = 0, max = 0;

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < depth; y++)
			{
				heigths[x, y] = CalculateHeights(x, y);
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
				heigths[x, y] = Mathf.InverseLerp(min, max, heigths[x, y]);
			}
		}

		return heigths;
	}

	private float CalculateHeights(int x, int y)
	{
		float xCoord = (float)x / width * scale;
		float yCoord = (float)y / width * scale;
		return _perlin.OctavePerlin(xCoord, yCoord, octaves, persistance, lacunarity);
	}
}
