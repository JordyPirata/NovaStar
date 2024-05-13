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
    public static Dictionary<float2, GameObject> chunkDictionary = new();
    public static List<GameObject> terrainChunksVisibleLastUpdate = new();
    public Transform viewer;
    public const float maxViewDst = 250;
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
            yield return new WaitForSeconds(2f);
            viewerPosition = new float2(viewer.position.x, viewer.position.z);
            viewerCoordinate = new float2(Mathf.RoundToInt(viewerPosition.x / width), Mathf.RoundToInt(viewerPosition.y / depth));
            UpdateVisibleChunks();
        }
        
    }
    public async void UpdateVisibleChunks()
    {
        for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
        {
            terrainChunksVisibleLastUpdate[i].SetActive(false);
        }
        terrainChunksVisibleLastUpdate.Clear();

        for (int yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
        {
            for (int xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
            {
                
                float2 viewedChunkCoord = new(viewerCoordinate.x + xOffset, viewerCoordinate.y + yOffset);
                
                if (chunkDictionary.ContainsKey(viewedChunkCoord))
                {
                    UpdateStatus(viewedChunkCoord);
                    if (chunkDictionary[viewedChunkCoord].activeInHierarchy)
                    {
                        terrainChunksVisibleLastUpdate.Add(chunkDictionary[viewedChunkCoord]);
                    }
                }
                else
                {
                    chunkDictionary.Add(viewedChunkCoord, await ChunkGenerator.GenerateChunk(viewedChunkCoord));
                }
            }
        }
    }
    public void UpdateStatus(float2 coords)
    {
        chunkDictionary[coords].GetInstanceID();
        Bounds bounds = new(chunkDictionary[coords].transform.position, Vector2.one * width);
        float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
        bool visible = viewerDstFromNearestEdge <= maxViewDst;

        chunkDictionary[coords].SetActive(visible);
    }
    
}

