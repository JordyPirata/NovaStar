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
    public static ChunkManager Instance;

    public Transform viewer;
    public Vector2 viewerPosition;
    private List<Chunk> chunks;
    // TODO: Make this a list of chunks
    public void Awake()
    {
        gameObject.AddComponent<ChunkGenerator>();
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
