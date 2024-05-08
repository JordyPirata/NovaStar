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

    public Material material;

    public const int octaves = 8;
    public const float persistance = Mathf.PI / 2;
    public const float lacunarity = .5f;
    public static int width = 33;
    public static int depth = 33;
    public static int length = width * depth;
    public static int height = 30;
    public const float offset = 0.01f;
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
        float2[] chunksCoords =
        {
            new(-1, 1),
            new(0, 1),
            new(-1, 0),
            new(0, 0),
            new(-1, -1),
            new(0, -1),
            new(1, 1),
            new(1, 0),
            new(1, -1),
            new(2, 1),
            new(2, 0),
            new(2, -1),
            new(2, 2),
            new(2, -2),
            new(1, 2),
            new(0, 2),
            new(-1, 2),
            new(-2, 2),
            new(-2, 1),
            new(-2, 0),
            new(-2, -1),
            new(-2, -2),
            new(-1, -2),
            new(0, -2),
            new(1, -2),
            new(2, -2),
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
