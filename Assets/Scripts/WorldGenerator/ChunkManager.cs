using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Generator
{
    //TODO: Change name to ChunkFacade and implement 
    public class ChunkManager : MonoBehaviour
    {
        public Transform viewer;
        private Vector2 viewerPosition;
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
}