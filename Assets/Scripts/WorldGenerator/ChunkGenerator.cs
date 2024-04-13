using UnityEngine;
using Repository;
using System.IO;
using System.Collections.Generic;

namespace Generator
{
    ///<summary>
    /// Class to generate the chunk object
    /// </summary>
    public class ChunkGenerator : MonoBehaviour
    {
        private string message;
        public readonly int poolSize = 10;
        public readonly GameObject ChunkPrefab;
        private readonly List<GameObject> chunkList;
        private ChunkGenerator instance;
        public ChunkGenerator Instance { get { return instance; } private set { instance = value; } }
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void Start()
        {
            AddChunkToPool(poolSize);
        }
        private void AddChunkToPool(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject chunk = Instantiate(ChunkPrefab);
                chunk.SetActive(false);
                chunkList.Add(chunk);
                chunk.transform.parent = transform;
            }
        }
        // TODO: 
        // generate chunkData with a job
        // Implement the method
        // create system to load existing chunks
        // implement system to save chunks
        // Test the system
        public GameObject GetChunk(int coordX, int CoordY)
        {
            foreach (GameObject chunk in chunkList)
            {
                if (!chunk.activeInHierarchy)
                {
                    chunk.SetActive(true);
                    return chunk;
                }
            }
            return null;
        }
        public Chunk Chunk { get; private set; }
        public void SetChunk(Chunk chunk)
        {
            Chunk = chunk;
            SetAttributes();
        }
        //set position of the chunks
        private void SetAttributes()
        {
            Terrain terrain = new Terrain();
            gameObject.transform.position = Chunk.position;
            gameObject.name = Chunk.ChunkName;
            terrain = TerrainSettings.ApplySettings(terrain, Chunk);
            gameObject.AddComponent<TerrainCollider>().terrainData = terrain.terrainData;
        }
        private async void SaveChunk()
        {
            message = await JsonRepository.Instance.CreateAsync(Chunk, Path.Combine(Application.persistentDataPath, string.Concat(Chunk.ChunkName, ".json")));
            Debug.Log(message);
        }
    }
}