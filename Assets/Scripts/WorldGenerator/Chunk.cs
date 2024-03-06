using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator
{
    public class Chunk
    {
        public Chunk()
        {
            IsLoaded = false;
            ChunkName = $"Chunk({CoordX},{CoordY})";
        }
        public string ChunkName;
        public int width = ChunkManager.width;
        public int height = ChunkManager.height;
        public Vector2 position;
        public int CoordX;
        public int CoordY;
        public bool IsLoaded;
        public float[,] heights;
        public float[,] temperatures;
        public float[,] moisture;
        
    }
}