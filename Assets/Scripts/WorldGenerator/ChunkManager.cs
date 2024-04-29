using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generator;
using Unity.Collections;
using Unity.Mathematics;

//TODO: Change name to ChunkFacade and implement 
public class ChunkManager : MonoBehaviour
{
    private static ChunkManager instance;
    public static ChunkManager Instance { get {return instance;} private set {instance = value;} }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public const int octaves = 8;
    public const float persistance = Mathf.PI / 2;
    public const float lacunarity = .5f;
    public const int width = 256;
    public const int depth = 256;
    public const int length = width * depth;
    public const int height = 20;
    public const float scale = 6.66f;
    public static int seed = 0;
    readonly SeedGenerator seedGenerator = new(seed);
    public int[] Permutation
    {
        get
        {
            return seedGenerator.permutation;
        }
    }

    public Transform viewer;
    public static Vector2 viewerPosition;
    // TODO: Make this a list of chunks
    public void Start()
    {
        // add component as a child of the chunk manager
        int2[] chunksCoords =
        {
            new(0, 0),
            new(0, 1),
            new(0,-1),
            new(1, 0),
            new(1, 1),
            new(1,-1),
            new(-1, 0),
            new(-1, 1),
            new(-1,-1)
        };
        
        StartCoroutine(UpdateViewerPosition());
        List<GameObject> chunks = ChunkPool.Instance.GetInactiveChunks();
        ChunkGenerator.Instance.GenerateChunk(chunks, chunksCoords);
    }
    IEnumerator Loadchunks()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
        }
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
