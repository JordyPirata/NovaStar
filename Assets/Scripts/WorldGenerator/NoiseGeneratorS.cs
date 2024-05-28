using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


namespace Generator
{
    public class NoiseGeneratorS : MonoBehaviour
    {
        int kernel;
        private struct PerlinData
        {
            public float2 coords;
            public float value;
        }

        public ComputeShader computeShader;
        private ComputeBuffer _computeBuffer;
        private int length;

        public void Start()
        {
            kernel = computeShader.FindKernel("CSMain");
            length = ChunkManager.Length;
        }

        public float[] GenerateNoise(float2 coords)
        {
            SetBuffer();
            computeShader.SetBuffer(0, "Result", _computeBuffer);
            computeShader.Dispatch(0, ChunkManager.Length / 257, 1, 1);

            var result = new float[ChunkManager.Length];
            
            _computeBuffer.GetData(result);
            _computeBuffer.Dispose();
            return result;
        }
        
        private void SetBuffer()
        {
            
            int coordSize = sizeof(float) * 2, heightSize = sizeof(float);
            int totalSize = coordSize + heightSize;

            _computeBuffer = new ComputeBuffer(ChunkManager.Length, totalSize);
            _computeBuffer.SetData(new PerlinData[ChunkManager.Length]);

        }
    }
}

