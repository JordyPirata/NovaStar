using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator
{
    public class Chunk : MonoBehaviour
    {
        public int width = 257;
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
        private Terrain _terrain;
		private TerrainCollider _terrainCollider;
		private TerrainData _terrainData;
        public float[,] heights;
        public float[,] temperatures;
        public float[,] moisture;
        
    }
}