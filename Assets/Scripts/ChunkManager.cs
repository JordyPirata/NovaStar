using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public Transform viewer;
    Stack<GameObject> chunks = new Stack<GameObject>();
    public void Awake()
    {
        // Create child from ChunkManager game object
        GameObject chunk = new($"Chunk({0}, {0})");
        // Set parent of chunk to ChunkManager
        chunk.transform.parent = transform;
        chunk.AddComponent<Chunk>();
        chunk.AddComponent<TerrainGenerator>();
    }
}
