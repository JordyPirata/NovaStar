using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace Generator
{
    public class ChunkGenerator
    {
        private Chunk chunk;
        NoiseGenerator noiseGenerator;

        private TerrainData GenerateTerrain(TerrainData terrainData)
        {
            terrainData.heightmapResolution = chunk.width;
            terrainData.size = new Vector3(chunk.width, 20, chunk.width);
            terrainData.SetHeights(0, 0, noiseGenerator.GenerateNoise());
            return terrainData;
        }
        private void Awake() {
            // Set name of chunk
            chunk.name = $"Chunk({CoordX}, {CoordY})";
            SetupChunk();
        }

        public void SetupChunk()
        {
            // Add terrain, terrain collider and terrain data to chunk
            _terrain = gameObject.AddComponent<Terrain>();
			_terrainCollider = gameObject.AddComponent<TerrainCollider>();
			_terrainData = new TerrainData();
			_terrainCollider.terrainData = _terrainData;
			_terrain.terrainData = _terrainData;
        }
    }
}