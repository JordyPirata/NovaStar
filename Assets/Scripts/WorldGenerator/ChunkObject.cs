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
        private Bounds Bounds { get; set; }
        private Vector2 Position { get; set; }
        private bool _isAvailable;
        public ChunkObject(GameObject gameObject)
        {
            GameObject = gameObject;
            _isAvailable = true;
        }

        public ChunkObject TryUse(Vector2 coord)
        {
            if (_isAvailable)
            {
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
    }
}