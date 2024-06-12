using Unity.Mathematics;
using UnityEngine;

namespace WorldGenerator
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
            if (!_isAvailable) return null;
            Coord = coord;
            Position = coord * ChunkConfig.width;
            Bounds = new(Position, Vector2.one * ChunkConfig.width);
            _isAvailable = false;
            return this;
        }
        // Release the chunk
        public void Release()
        {
            _isAvailable = true;
        }
        // Update the status of the chunk
        public bool UpdateStatus()
        {
            // get the distance from the viewer to the nearest edge of the chunk
            var viewerDstFromNearestEdge = Mathf.Sqrt(Bounds.SqrDistance(ChunkConfig.viewerPosition));
            var visible = viewerDstFromNearestEdge <= ChunkConfig.maxViewDst;
            // set the visibility of the chunk
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
            // if the distance from the viewer to the chunk is greater than the max view distance
            if (!(Vector2.Distance(Position, ChunkConfig.viewerPosition) >
                  ChunkConfig.maxViewDst + ChunkConfig.width * 2)) return false;
            Release();
            return true;
        }
    }
}