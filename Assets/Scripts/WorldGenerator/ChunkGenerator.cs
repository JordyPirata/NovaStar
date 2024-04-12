using UnityEngine;
using Repository;
using System.IO;

namespace Generator
{
    public class ChunkGenerator : MonoBehaviour
    {
        private string message;
        private Terrain terrain = new();
        private Chunk chunk = ChunkFactory.CreateChunk(0, 1);

        public void Awake( )
        {
            SetAttributes();
            GenerateTerrain();
            SaveChunk();
        }
        //set position of the chunks
        public void SetAttributes( )
        {
            gameObject.transform.position = chunk.position;
            gameObject.name = chunk.ChunkName;
        }
        public void GenerateTerrain( )
        {
            
            terrain = gameObject.AddComponent<Terrain>();
            terrain = TerrainSettings.ApplySettings(terrain, chunk);
            gameObject.AddComponent<TerrainCollider>().terrainData = terrain.terrainData;

        }
        public async void SaveChunk( )
        {
            message = await JsonRepository.Instance.CreateAsync(chunk, Path.Combine(Application.persistentDataPath, string.Concat(chunk.ChunkName, ".json")));
            Debug.Log(message);
        }
    }
}