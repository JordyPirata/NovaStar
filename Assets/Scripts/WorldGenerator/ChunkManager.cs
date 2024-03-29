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
    public const int width = 257;
    public const int depth = 257;
    public const int height = 20;
    public const float scale = 6.66f;
    public const int seed = 0;
    public static ChunkManager Instance;
    public Transform viewer;
    public static Vector2 viewerPosition;
    private List<Chunk> chunks;
    // TODO: Make this a list of chunks
    public void Start()
    {
        // add component as a child of the chunk manager
        GameObject chunk = new();
        chunk.transform.parent = transform;
        chunk.AddComponent<ChunkGenerator>().SetPosition(viewerPosition);
        StartCoroutine(UpdateViewerPosition());
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
    IEnumerator UpdateViewerPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        }
    }
}
