using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generator;
using Unity.Collections;
using Unity.Mathematics;
using System.Threading.Tasks;

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
    public static int width = 65;
    public static int depth = 65;
    public static int length = width * depth;
    public static int height = 30;
    public const float offset = 0.01f;
    public static int seed = 0;
    private Dictionary<float2, GameObject> chunkDictionary = new();
    public Transform viewer;
    public const float maxViewDst = 100;
    private readonly int chunkVisibleInViewDst = Mathf.RoundToInt(maxViewDst / width);
    public Vector2 viewerPosition;
    public float2 viewerCoordinate;

    // TODO: Make this a list of chunks
    public void Start()
    {
        // add component as a child of the chunk manager
        
        StartCoroutine(UpdateViewer());
        StartCoroutine(UpdateInactiveChunks());
        UpdateVisibleChunks();
    }

    IEnumerator UpdateInactiveChunks()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            ChunkPool.Instance.UpdateInactiveChunks();
        }
    }
    IEnumerator UpdateChunks()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            UpdateVisibleChunks();
        }
        
    }
    public void UpdateVisibleChunks()
    {
        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / width);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / width);

        for (int yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
        {
            for (int xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
            {
                float2 viewedChunkCoord = new(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
                ChunkGenerator.GenerateChunk(viewedChunkCoord);
                /*
                if (chunkDictionary.ContainsKey(viewedChunkCoord))
                {
                    
                    Bounds bounds = new(chunkDictionary[viewedChunkCoord].transform.position, Vector3.one * width);
                    float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
                    bool visible = viewerDstFromNearestEdge <= maxViewDst;
                    chunkDictionary[viewedChunkCoord].SetActive(visible);
                    
                }
                else
                {
                    chunkDictionary.Add(viewedChunkCoord, ChunkGenerator.GenerateChunk(viewedChunkCoord));
                }*/
            }
        }
    }
    IEnumerator UpdateViewer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            viewerPosition = new float2(viewer.position.x, viewer.position.z);
            viewerCoordinate = new float2(Mathf.FloorToInt(viewerPosition.x / width), Mathf.FloorToInt(viewerPosition.y / depth));
        }
    }
}
