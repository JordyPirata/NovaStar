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
	
	public int width = 256;
	public int depth = 256;
	public int height = 4;
	public float scale;
	public int seed;

	private void Start()
    {
	    _perlin.SetSeed(seed);
	    _terrain = GetComponent<Terrain>();
	    _terrain.terrainData = GenerateTerrain( _terrain.terrainData);
    }

    private TerrainData	 GenerateTerrain(TerrainData terrainData)
    {
	    terrainData.heightmapResolution = width + 1;
	    terrainData.size = new Vector3(width, height, depth);
	    terrainData.SetHeights(0,0,GenerateHeights());
	    return terrainData;
    }

    private float[,] GenerateHeights()
    {
	    float [,] heigths = new float [width,depth], min, max;

	    for (int x = 0; x < width; x++)
	    {
		    for (int y = 0; y < depth; y++)
		    {
			    heigths[x, y] = CalculateHeights(x, y);
			    // TODO: Normalize noise
		    }
	    }

	    return heigths;
    }

    private float CalculateHeights(int x, int y)
    {
	    float xCoord = (float)x / width * scale;
	    float yCoord = (float)y / width * scale;
	    return _perlin.CalculatePerlin(xCoord, yCoord);
    }
}
