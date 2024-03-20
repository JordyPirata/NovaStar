using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator
{
    public struct Chunk
    {
        
        public string ChunkName;
        public int width;
        public int depth;
        public int height;
        [NonSerialized]public Vector2 position;
        public int CoordX;
        public int CoordY;
        public bool IsLoaded;
        public float[] heights;
        public float[] temperatures;
        public float[] moisture;
        
    }
}