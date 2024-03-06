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
        private readonly Chunk chunk = new();
        public NoiseGenerator noiseGenerator;
        public TerrainData terrainData;
        private Terrain terrain;
        private void Start()
        {
            SetUpChunk();
            GenerateTerrain();
        }
        public void Update()
        {
            SetPosition(ChunkManager.viewerPosition);
        }
        //set position of the chunk
        public void SetPosition(Vector2 position)
        {
            chunk.CoordX = (int)position.x / chunk.width;
            chunk.CoordY = (int)position.y / chunk.width;
            chunk.ChunkName = $"Chunk({chunk.CoordX},{chunk.CoordY})";
            chunk.position = new Vector2(chunk.CoordX * chunk.width, chunk.CoordY * chunk.width);
            gameObject.transform.position = new Vector3(chunk.position.x, 0, chunk.position.y);
            gameObject.name = chunk.ChunkName;
        }
        public void GenerateTerrain()
        {
            chunk.heights = noiseGenerator.GenerateNoise(chunk.CoordX, chunk.CoordY);
            terrainData.heightmapResolution = chunk.width;
            terrainData.size = new Vector3(chunk.width, chunk.height, chunk.width);
            terrainData.SetHeights(0, 0, chunk.heights);
        }

        public void SetUpChunk()
        {

            //GENERATE NEW TERRAINDATA
            terrainData = new TerrainData();
            //terrain.materialTemplate = new Material(Shader.Find(""));
            gameObject.AddComponent<Terrain>().terrainData = terrainData;
            gameObject.AddComponent<TerrainCollider>().terrainData = terrainData;
            noiseGenerator = new NoiseGenerator();
        }
        public void SaveChunk()
        {

        }
    }
}