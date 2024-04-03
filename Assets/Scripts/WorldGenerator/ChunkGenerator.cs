using UnityEngine;
using Repository;
using System.IO;
using Util;


namespace Generator
{
    public class ChunkGenerator : MonoBehaviour
    {
        private string message;
        private NoiseGenerator noiseGenerator;
        private TerrainData terrainData;
        private Terrain terrain;
        private Chunk chunk = ChunkFactory.CreateChunk(0, 0);

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
            gameObject.transform.position = new Vector3(chunk.CoordX, 0, chunk.CoordY);
            gameObject.name = chunk.ChunkName;
        }
        public void GenerateTerrain()
        {
            terrain.terrainData.baseMapResolution = chunk.width;
            chunk.heights = noiseGenerator.GenerateNoise(chunk.CoordX, chunk.CoordY);
            terrainData.baseMapResolution = chunk.width + 1;
            terrainData.heightmapResolution = chunk.width + 1;
            terrainData.size = new Vector3(chunk.width, chunk.height, chunk.width);
            terrainData.SetHeights(0, 0,TransferData.TransferDataFromArrayTo2DArray(chunk.heights,chunk.width,chunk.depth));
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