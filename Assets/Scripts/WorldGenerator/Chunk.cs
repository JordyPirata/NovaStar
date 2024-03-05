using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator
{
    public class Chunk : MonoBehaviour
    {
        public int width = ChunkManager.Instance.width;
        public int height = ChunkManager.Instance.height;
        public int CoordX;
        public int CoordY;
        public bool IsLoaded
        {
            get => IsLoaded;
            set 
            {
                // Active game object if value is true
                gameObject.SetActive(value);
                IsLoaded = value;
            }
        }
        public Terrain _terrain;
        public TerrainCollider _terrainCollider;      
        public float[,] heights;
        public float[,] temperatures;
        public float[,] moisture;
        
    }
}