using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator
{
    public class Chunk : MonoBehaviour
    {
        Vector2 position;
        public int CoordX;
        public int CoordY;
        public bool IsLoaded
        {
            get => IsLoaded;
            set 
            {
                // Active game object if value is true
                gameObject.SetActive(value);
                IsLoaded = value;
            }
        }
        public float[,] heights;
        public float[,] temperatures;
        public float[,] moisture;
        private void Awake() {
            // Set name of chunk
            gameObject.name = $"Chunk({CoordX}, {CoordY})";
        }
    }
}