using System.Diagnostics.Tracing;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace Generator
{
    public class ChunkObject
    {
        public GameObject GameObject { get; set; }
        public Terrain Terrain { get; set; }
        public float2 Coord { get; set; }
        private Bounds Bounds { get; set; }
        private Vector2 Position { get; set; }
        private bool _isAvailable;
        public ChunkObject(GameObject gameObject)
        {

            GameObject = gameObject;
            Terrain = GameObject.GetComponent<Terrain>();
            _isAvailable = true;
        }

        public ChunkObject TryUse(float2 coord)
        {
            if (_isAvailable)
            {
                Coord = coord;
                Position = coord * ChunkManager.width;
                Bounds = new(Position, Vector2.one * ChunkManager.width);
                _isAvailable = false;
                return this;
            }
            return null;
        }

        public void Release()
        {
            _isAvailable = true;
        }
        public bool UpdateStatus()
        {
            float viewerDstFromNearestEdge = Mathf.Sqrt(Bounds.SqrDistance(ChunkManager.viewerPosition));
            bool visible = viewerDstFromNearestEdge <= ChunkManager.maxViewDst;
            SetVisible(visible);
            return visible;

        }
        public void SetVisible(bool visible)
        {
            GameObject.SetActive(visible);
        }
        public bool IsVisible()
        {
            return GameObject.activeSelf;
        }
        public bool CheckDistanceAndRelease()
        {
            if (Vector2.Distance(Position, ChunkManager.viewerPosition) > ChunkManager.maxViewDst + ChunkManager.width + 100)
            {
                Release();
                return true;
            }
            return false;
        }
    }
}