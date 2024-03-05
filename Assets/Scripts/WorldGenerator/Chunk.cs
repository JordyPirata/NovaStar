using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator
{
    public class Chunk : MonoBehaviour
    {
        public int width = ChunkManager.Instance.width;
        public int height = ChunkManager.Instance.height;
        public int CoordX = 0;
        public int CoordY = 0;
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
        public float[,] heights;
        public float[,] temperatures;
        public float[,] moisture;
        
    }
}