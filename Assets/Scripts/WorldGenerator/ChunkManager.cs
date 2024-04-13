using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Generator;
using Repository;
using System.Threading.Tasks;
using Unity.Collections;

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

    readonly SeedGenerator seedGenerator = new(seed);
    public const int width = 256;
    public const int depth = 256;
    public const int height = 20;
    public const float scale = 6.66f;
    public static int seed = 0;
    public int[] Permutation
    {
        get
        {
            return seedGenerator.permutation;
        }
    }

    public Transform viewer;
    public static Vector2 viewerPosition;
    private NativeArray<Chunk> chunks;
    // TODO: Make this a list of chunks
    public void Start()
    {
        // add component as a child of the chunk manager

        StartCoroutine(UpdateViewerPosition());
    }
    IEnumerator Loadchunks()
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
