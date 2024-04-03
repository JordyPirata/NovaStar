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
        private Chunk chunk = ChunkFactory.CreateChunk(0, 1);

        private void Start()
        {
            SetUpChunk();
            SetPosition();
            GenerateTerrain();
            SaveChunk();

        }
        //set position of the chunk
        public void SetPosition()
        {
            gameObject.transform.position = new Vector3(chunk.CoordX * chunk.width, 0, chunk.CoordY * chunk.depth);
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