using System;
using UnityEngine;
using Random = System.Random;
public class Perlin 
{
	// Generate Octave Perlin noise value for double coordinates x, y
	public double OctavePerlin(double x, double y, int octaves, double persistence, double lacunarity)
	{

		double total = 0;
		double frequency = 1;
		double amplitude = 1;
		for (int i = 0; i < octaves; i++)
		{
			total += CalculatePerlin(x * frequency, y * frequency) * amplitude;

			amplitude *= persistence;
			frequency *= lacunarity;
		}
	
		return total;
	}
	// set a seed value for the permutation vector
	private int seed = 0;
	public void SetSeed(int seed)
	{
		this.seed = seed;
		
		for (int i = 0; i < 256; i++)
		{
			p[256 + i] = p[i] = Permutation()[i];
		}
	}
	//Generate a new permutation vector based on the value of seed
	public int[] Permutation()
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
	// Constructor
	public Perlin()
	{
		SetSeed(0);
	}
	private readonly int[] p = new int[512];
	// Calculate Perlin noise value for coordinates x, y
	public double CalculatePerlin(double x, double y)
	{
		int X = (int)Math.Floor(x) & 255, // Keep the first 8 bits of the integer (coerce to 0-255)
			Y = (int)Math.Floor(y) & 255;

		x -= Math.Floor(x); // Get the decimal part of the number
		y -= Math.Floor(y);

		double u = Fade(x), // Compute the fade curves for x, y
			   v = Fade(y);

		int A = p[X] + Y, AA = p[A], AB = p[A + 1],  // Hash coordinates of the 4 corners of the unit square
			B = p[X + 1] + Y, BA = p[B], BB = p[B + 1];   
		
		return Lerp(v,											 			
				Lerp(u, Grad(p[AA], x, y), Grad(p[BA], x - 1, y)),			
				Lerp(u, Grad(p[AB], x, y - 1), Grad(p[BB], x - 1, y - 1))); // Add blended results from 4 corners of the unit square
	}

	private static double Fade(double t) { return t * t * t * (t * (t * 6 - 15) + 10); } // 6t^5 - 15t^4 + 10t^3 (smooth interpolation curve)
	private static double Lerp(double t, double a, double b) { return a + t * (b - a); } // linear interpolation
	private static double Grad(int hash, double x, double y)
	{
		int h = hash & 15;									// CONVERT LO 4 BITS OF HASH CODE
		double u = h < 8 ? x : y,							// into 12 gradient directions
			   v = h < 4 ? y : h == 12 || h == 14 ? x : y;
		return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v); 
	}
}
