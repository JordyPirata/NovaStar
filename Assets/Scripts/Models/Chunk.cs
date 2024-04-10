using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Generator
{
    public struct Chunk
    {
        [NonSerialized]
        public float3 position;
        public string ChunkName;
        public int width;
        public int depth;
        public int height;
        public int CoordX;
        public int CoordY;
        public bool IsLoaded;
        public float[] heights;
        public float[] temperatures;
        public float[] moisture;
        
    }
}