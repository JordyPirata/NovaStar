using System.Diagnostics.Tracing;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

namespace Generator
{
    public class PoolItem
    {
        public GameObject GameObject { get; set; }
        private Bounds Bounds { get; set; }
        private bool _isAvailable;
        public PoolItem(GameObject gameObject)
        {
            GameObject = gameObject;
            _isAvailable = true;
        }

        public PoolItem TryUse()
        {
            if (_isAvailable)
            {
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
            Bounds = new(GameObject.transform.position, Vector2.one * ChunkManager.width);
            float viewerDstFromNearestEdge = Mathf.Sqrt(Bounds.SqrDistance(ChunkManager.viewerPosition));
            bool visible = viewerDstFromNearestEdge <= ChunkManager.maxViewDst;

            SetVisible(visible);
            if (!visible)
            {
                Release();
            }
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