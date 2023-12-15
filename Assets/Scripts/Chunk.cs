using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public int x;
    public int y;
    public bool isLoaded;
    float[,] temperatures;
    float[,] moisture;

    public void Load()
    {
        isLoaded = true;
    }
    public void Unload()
    {
        isLoaded = false;
    }
}
