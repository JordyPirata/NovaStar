using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Generator;
using Repository;

//TODO: Change name to ChunkFacade and implement 
public class ChunkManager : MonoBehaviour
{
    public int width = 257;
    public int depth = 257;
    public int height = 20;
    public float scale = 6.66f;
    public int seed = 0;
    private static ChunkManager instance;
    public static ChunkManager Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }
    public Transform viewer;
    public Vector2 viewerPosition;
    private List<Chunk> chunks;
    // TODO: Make this a list of chunks
    public void Awake()
    {
        // Create child from ChunkManager game object
        GameObject chunk = new($"Chunk({0}, {0})");
        // Set parent of chunk to ChunkManager
        chunk.transform.parent = transform;
        chunk.AddComponent<Chunk>();
        chunk.AddComponent<NoiseGenerator>();
    }
    IEnumerator LoadChunks()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            foreach (Chunk chunk in chunks)
            {

            }
        }
    }
    public void Start()
    {
        StartCoroutine(UpdateViewerPosition());
    }
    IEnumerator UpdateViewerPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        }
    }

}
