using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


namespace Generator
{
    public class NoiseGeneratorS
    {
        struct NoiseData
        {
            public float2 coords;
            public float height;
        }
        private static ComputeShader _computeShader = Resources.Load<ComputeShader>("Assets/Scripts/NoiseGenerator.compute");
        private static ComputeBuffer _computeBuffer;

        public static float[] GenerateNoise(float2 coords)
        {
            int coordSize = sizeof(float) * 2, heightSize = sizeof(float);
            int totalSize = coordSize + heightSize;

            _computeBuffer = new ComputeBuffer(ChunkManager.length, totalSize);
            _computeBuffer.SetData(new NoiseData[ChunkManager.length]);

            _computeShader.SetBuffer(0, "Result", _computeBuffer);
            _computeShader.Dispatch(0, 257, 257, 1);

            var result = new float[ChunkManager.length];
            
            _computeBuffer.GetData(result);
            _computeBuffer.Dispose();
            return result;
        }


    }
}

