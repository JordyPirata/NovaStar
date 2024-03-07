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
using Repository;
using System.IO;


namespace Generator
{
    public class ChunkGenerator : MonoBehaviour
    {
        private string message;
        private readonly Chunk chunk = new();
        private NoiseGenerator noiseGenerator;
        private TerrainData terrainData;
        private Terrain terrain;
        private void Start()
        {
            SetUpChunk();
            SetPosition(ChunkManager.viewerPosition);
            GenerateTerrain();
            SaveChunk();

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
            terrain.terrainData.baseMapResolution = chunk.width;
            chunk.heights = noiseGenerator.GenerateNoise((int)chunk.position.x, (int)chunk.position.y);
            terrainData.baseMapResolution = chunk.width + 1;
            terrainData.heightmapResolution = chunk.width + 1;
            terrainData.size = new Vector3(chunk.width, chunk.height, chunk.width);
            terrainData.SetHeights(0, 0, chunk.heights);
        }

        public void SetUpChunk()
        {
            //GENERATE NEW TERRAINDATA
            terrainData = new TerrainData();
            //terrain.materialTemplate = new Material(Shader.Find(""));
            terrain = gameObject.AddComponent<Terrain>();
            terrain.terrainData = terrainData;
            TerrainSettings.ApplySettings(terrain);
            gameObject.AddComponent<TerrainCollider>().terrainData = terrainData;
            noiseGenerator = new NoiseGenerator();
        }
        public async void SaveChunk()
        {
            message = await JsonRepository.Instance.CreateAsync(chunk, Path.Combine(Application.persistentDataPath, "chunks.json"));
            Debug.Log(message);
        }
    }
}