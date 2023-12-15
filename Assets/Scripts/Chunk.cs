using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    Vector2 position;
    int CoordX;
    int CoordY;
    public bool isLoaded;
    float[,] heights;
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
