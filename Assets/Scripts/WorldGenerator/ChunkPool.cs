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
        [SerializeField] private int poolSize = 100;
        [SerializeField] private GameObject ChunkPrefab;
        private readonly List<PoolItem> chunkList = new();
        private static ChunkPool instance;
        public static ChunkPool Instance { get { return instance; } }
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
            InitPool();
        }
        private void InitPool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject chunk = Instantiate(ChunkPrefab);
                chunk.SetActive(false);
                chunkList.Add(new(chunk));
                // Set the parent of the chunk to the chunk pool
                chunk.transform.SetParent(transform);
            }
        }

        public PoolItem GetChunk()
        {

            // for (int i = 0; i < poolSize; i++)
            // {
            //     if (!chunkList[i].activeInHierarchy)
            //     {
            //         return chunkList[i];
            //     }
            // }
            // return null;

            foreach (var chunk in chunkList)
            {
                var chunkGameObject = chunk.TryUse();
                if (chunkGameObject != null)
                {
                    return chunkGameObject;
                }
            }
            return null;
        }
        
    }
}