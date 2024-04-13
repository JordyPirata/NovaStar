using UnityEngine;
using Repository;
using System.IO;
using System.Collections.Generic;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;

namespace Generator
{
    ///<summary>
    /// This class is responsible for generating chunks and adding them to the pool
    /// </summary>
    public class ChunkPool : MonoBehaviour
    {
        private string message;
        [SerializeField] private int poolSize = 24;
        [SerializeField] private GameObject ChunkPrefab;
        private readonly List<GameObject> chunkList = new();
        private ChunkPool instance;
        public ChunkPool Instance { get { return instance; } private set { instance = value; } }
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
        public List<GameObject> GetInactiveChunks(List<int2> chunksCoords)
        {
            int coordsCount = chunksCoords.Count;
            // Set empty list of inactive chunks
            List<GameObject> inactiveChunks = new();
            for (int i = 0; i < chunkList.Count; i++)
            {
                if (!chunkList[i].activeInHierarchy)
                {
                    inactiveChunks.Add(chunkList[i]);
                }
            }
            if (coordsCount > inactiveChunks.Count)
            {
                return null;
            }

            return null;
        }


        
    }
}