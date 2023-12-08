using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
public class TerrainGenerator : MonoBehaviour
{
	private Perlin _perlin = new();
	private Terrain _terrain;
	
	public int width = 255;
	public int depth = 255;
	public int height = 10;
	public float scale;
	public int seed;
	[Range(1,10)]
	public int octaves;
	[Range(0, 2)]
	public float persistance;
	[Range(0, 2)]
	public float lacunarity;
	
	private void Start()
	{
		_perlin.SetSeed(seed);
		_terrain = GetComponent<Terrain>();
		_terrain.terrainData = GenerateTerrain( _terrain.terrainData);
	}
	private void Update()
    {
	    _perlin.SetSeed(seed);
	    _terrain.terrainData = GenerateTerrain( _terrain.terrainData);
    }

    private TerrainData	GenerateTerrain(TerrainData terrainData)
    {
	    terrainData.heightmapResolution = width + 1;
	    terrainData.size = new Vector3(width, height, depth);
	    terrainData.SetHeights(0,0,GenerateHeights());
	    return terrainData;
    }

    private float[,] GenerateHeights()
    {
	    float[,] heigths = new float [width, depth];
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
