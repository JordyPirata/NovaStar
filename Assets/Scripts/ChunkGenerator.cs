using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
public struct ChunkAccess
{
    public float[,] heights;
    public float[,] temperatures;
    public float[,] moisture;
    public int CoordX;
    public int CoordY;
    public bool isLoaded;
    public void Load()
    {
        isLoaded = true;
    }
    public void Unload()
    {
        isLoaded = false;
    }
}
public struct ChunkGenerator : IJobParallelForTransform
{

    public void Execute(int index, TransformAccess transform)
    {
        throw new System.NotImplementedException();
    }
}
