using System;
using UnityEngine;
using Unity.Collections;
using Random = System.Random;
using Repository;
public class SeedGenerator 
{
	public SeedGenerator(int seed)
	{
		SetSeed(seed);
	}
	public int[] p = new int[512];
    private int seed;
	public void SetSeed(int seed)
	{
		this.seed = seed;
		
		for (int i = 0; i < 256; i++)
		{
			p[256 + i] = p[i] = Permutation()[i];
		}
	}
	//Generate a new permutation vector based on the value of seed
	private int[] Permutation()
	{
		for (int i = 0; i <= 255; i++)
		{
			p[i] = i;
		}
		Random random = new(seed);
		// Fisher-Yates shuffle algorithm
		for (int i = p.Length - 1; i > 0; i--)
		{
			int j = random.Next(0, i + 1);
			// Swap p[i] and p[j]
			(p[j], p[i]) = (p[i], p[j]);
		}
		return p;
	}
}