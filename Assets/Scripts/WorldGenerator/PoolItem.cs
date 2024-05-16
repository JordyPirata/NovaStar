using System.Diagnostics.Tracing;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace Generator
{
    public class PoolItem
    {
        public GameObject GameObject { get; set; }
        private Bounds bounds { get; set; }
        private Vector2 position { get; set; }
        private bool _isAvailable;
        public PoolItem(GameObject gameObject )
        {
            
            GameObject = gameObject;
            _isAvailable = true;
        }

        public PoolItem TryUse(Vector2 coord)
        {
            if (_isAvailable)
            {
                position = coord * ChunkManager.width;
                bounds = new(position, Vector2.one * ChunkManager.width);
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
            float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(ChunkManager.viewerPosition));
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