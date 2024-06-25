using Unity.Mathematics;
using UnityEngine;


namespace WorldGenerator
{
    public class NoiseGeneratorS 
    {
        // Singleton
        private static NoiseGeneratorS instance;
        public static NoiseGeneratorS Instance => instance ??= new NoiseGeneratorS();
        private readonly ComputeShader computeShader = Resources.Load<ComputeShader>("NoiseGenerator");
        private readonly int length = ChunkConfig.Length;
        private readonly int seed = ChunkConfig.seed;
        private static readonly int Coords = Shader.PropertyToID("coords");
        private static int kernel;
        private static readonly int Values = Shader.PropertyToID("values");
        private static readonly int Seed = Shader.PropertyToID("seed");
        public float[] GenerateNoise(float2 coords)
        {
            // Get the kernel
            kernel = computeShader.FindKernel("CSMain");
            // Calculate the initial x and y
			var iCoordX = (int)coords.x * ChunkConfig.width - (int)coords.x;
			var iCoordY = (int)coords.y * ChunkConfig.depth - (int)coords.y;
            
            var allCoords = new float2[ChunkConfig.Length];
            var heights = new float[ChunkConfig.Length];
            int initialY = iCoordY, i = 0;

			for (var y = 0; y < ChunkConfig.width; y++)
			{
				for (var x = 0; x < ChunkConfig.depth; x++)
				{
					// Calculate the actual x and y
					allCoords[i] = new int2(iCoordX, iCoordY);
					// is negative
					iCoordY++;
					i++;
				}
				iCoordX++;
				// Reset the y
				iCoordY = initialY;
			}

            // Initialize the buffer
            var coordsBuffer = InitCoordsBuffer(allCoords);
            var valuesBuffer = new ComputeBuffer(length, sizeof(float));
			
            // Set the buffer
            computeShader.SetBuffer(kernel, Coords, coordsBuffer);
            computeShader.SetBuffer(kernel, Values, valuesBuffer);
            computeShader.SetInt(Seed, seed);
            // Dispatch the shader
            computeShader.Dispatch(kernel, Mathf.CeilToInt(length / 52f), 1, 1); 
            
            // Get the data from the buffer
            valuesBuffer.GetData(heights);
            
            // Release the buffers
            coordsBuffer.Release();
            valuesBuffer.Release();
            return heights;
        }
        
        private ComputeBuffer InitCoordsBuffer(float2[] data)
        {
	        var totalSize = sizeof(float) * 2; 

            var computeBuffer = new ComputeBuffer(data.Length, totalSize);
            computeBuffer.SetData(data);

            return computeBuffer;
        }
    }
}

