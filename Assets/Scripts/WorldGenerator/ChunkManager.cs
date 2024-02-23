using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public Transform viewer;
    private Vector2 viewerPosition;
    private int chunkSize = 255;
    private int chunkVisibleInViewDistance = 1;

    private Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();
    List<Chunk> terrainChunksVisibleLastUpdate = new List<Chunk>();

    public void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
    }
    public void Awake()
    {
        // Create child from ChunkManager game object
        GameObject chunk = new($"Chunk({0}, {0})");
        // Set parent of chunk to ChunkManager
        chunk.transform.parent = transform;
        chunk.AddComponent<Chunk>();
        chunk.AddComponent<NoiseGenerator>();
    }
}
