using Random = System.Random;
public class SeedGenerator
{
	public SeedGenerator(int seed)
	{
		this.seed = seed;
		SetPermutation(seed);
	}
	/// <summary>
	/// Permutation array used for generating noise
	/// </summary>
	public int[] permutation = new int[512];
	private readonly int seed;
	private void SetPermutation(int seed)
	{
		for (int i = 0; i < 256; i++)
		{
			permutation[256 + i] = permutation[i] = Permutation()[i];
		}
	}
	//Generate a new permutation vector based on the value of seed
	private int[] Permutation()
	{
		for (int i = 0; i <= 255; i++)
		{
			permutation[i] = i;
		}
		Random random = new(seed);
		// Fisher-Yates shuffle algorithm
		for (int i = permutation.Length - 1; i > 0; i--)
		{
			int j = random.Next(0, i + 1);
			// Swap p[i] and p[j]
			(permutation[j], permutation[i]) = (permutation[i], permutation[j]);
		}
		return permutation;
	}
}