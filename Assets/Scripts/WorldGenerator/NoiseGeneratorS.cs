using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;


namespace Generator
{
    public class NoiseGeneratorS : MonoBehaviour
    {
        private static NoiseGeneratorS instance;
        public static NoiseGeneratorS Instance { get { return instance; } }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        public ComputeShader computeShader;
        private readonly int length = ChunkManager.Length;
        private static readonly int Result = Shader.PropertyToID("coords");
        private static int kernel;

        public void Start()
        {
	        kernel = computeShader.FindKernel("CSmain");
        }
        public float[] GenerateNoise(float2 coords)
        {
            // Calculate the initial x and y
			var iCoordX = (int)coords.x * ChunkManager.width - (int)coords.x;
			var iCoordY = (int)coords.y * ChunkManager.depth - (int)coords.y;
            
            var allCoords = new float2[ChunkManager.Length];
            var heights = new float[ChunkManager.Length];
            int initialY = iCoordY, i = 0;

			for (var y = 0; y < ChunkManager.width; y++)
			{
				for (var x = 0; x < ChunkManager.depth; x++)
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
            ComputeBuffer coordsBuffer = InitCoordsBuffer(allCoords);
			
            // Set the buffer
            computeShader.SetBuffer(kernel, Result, coordsBuffer);
            computeShader.Dispatch(kernel, length / 257, 1, 1);
            
            coordsBuffer.Release();
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

