using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Jobs;
using Unity.VisualScripting;

namespace Generator
{
    public class ChunkGenerator : MonoBehaviour
    {
        private Chunk chunk;
        private NoiseGenerator noiseGenerator;
        private TerrainData terrainData;
        
        public void GenerateTerrain()
        {
            terrainData.heightmapResolution = chunk.width;
            terrainData.size = new Vector3(chunk.width, chunk.height, chunk.width);
            terrainData.SetHeights(0, 0, noiseGenerator.GenerateNoise(chunk.CoordX, chunk.CoordY));
        }

        public void SetUpChunk()
        {
            
        }
        
    }

}