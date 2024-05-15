using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct Chunk
{
    [NonSerialized]
    public float3 position;
    public string ChunkName;
    public int width;
    public int depth;
    public int height;
    public float[] heights;
}
