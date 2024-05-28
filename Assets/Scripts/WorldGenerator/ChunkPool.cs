using UnityEngine;
using System.Collections.Generic;
using Map = Generator.ChunkGrid<Generator.ChunkObject>;

namespace Generator
{
    ///<summary>
    /// This class is responsible for generating chunks and adding them to the pool
    /// </summary>
    public class ChunkPool : MonoBehaviour
    {
        [SerializeField] private int poolSize;
        [SerializeField] private GameObject ChunkPrefab;
        private readonly List<ChunkObject> chunkList = new();
        private static ChunkPool instance;
        public static ChunkPool Instance { get { return instance; } }
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            InitPool();
        }
        private void InitPool()
        {
            for (var i = 0; i < poolSize; i++)
            {
                GameObject chunk = Instantiate(ChunkPrefab, transform, true);
                chunk.SetActive(false);
                chunkList.Add(new(chunk));
                // Set the parent of the chunk to the chunk pool
            }
        }
        public ChunkObject GetChunk(Vector2 coord)
        {
            for (var i = 0; i < chunkList.Count; i++)
            {
                if(chunkList[i].CheckDistanceAndRelease())
                {
                    Map.Remove(chunkList[i].Coord);
                }
                var chunkGameObject = chunkList[i].TryUse(coord);
                if (chunkGameObject != null)
                {
                    return chunkGameObject;
                }
            }
            return null;
        }
    }
}