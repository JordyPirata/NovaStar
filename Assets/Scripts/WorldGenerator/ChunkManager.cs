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
    public static int width = 257;
    public static int depth = 257;
    public static int length = width * depth;
    public static int height = 30;
    public const float offset = 0.01f;
    public static int seed = 0;
    public static Dictionary<float2, PoolItem> chunkDictionary = new();
    public static List<PoolItem> chunksVisible = new();
    public Transform viewer;
    public const float maxViewDst = 450;
    private readonly int chunkVisibleInViewDst = Mathf.RoundToInt(maxViewDst / width);
    public static Vector2 viewerPosition;
    public float2 viewerCoordinate;

    public void Start()
    {
        StartCoroutine(UpdateChunks());
    }
    
    IEnumerator UpdateChunks()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            viewerPosition = new float2(viewer.position.x, viewer.position.z);
            viewerCoordinate = new float2(Mathf.RoundToInt(viewerPosition.x / width), Mathf.RoundToInt(viewerPosition.y / depth));
            UpdateVisibleChunks();
        }
        
    }
    public void UpdateVisibleChunks()
    {
        /*
        foreach (var chunks in chunksVisible)
        {
            //chunks.SetVisible(false);
        }
        chunksVisible.Clear();
        */
        for (int yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
        {
            for (int xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
            {
                
                float2 viewedChunkCoord = new(viewerCoordinate.x + xOffset, viewerCoordinate.y + yOffset);
                if (chunkDictionary.ContainsKey(viewedChunkCoord))
                {
                    chunkDictionary[viewedChunkCoord].UpdateStatus();
                    if (chunkDictionary[viewedChunkCoord].IsVisible())
                    {
                        chunksVisible.Add(chunkDictionary[viewedChunkCoord]);
                    }
                }
                else
                {
                    chunkDictionary.Add(viewedChunkCoord, ChunkGenerator.GenerateChunk(viewedChunkCoord));
                }

            }
        }
    }  
}

