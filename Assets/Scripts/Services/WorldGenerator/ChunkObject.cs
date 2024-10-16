using Config;
using Models;
using Services.Interfaces;
using Unity.Mathematics;
using UnityEngine;

namespace Services.WorldGenerator
{
    // Make it null-able type
    public class ChunkObject
    {
        public Chunk ChunkData { get; set; }
        public GameObject GameObject { get; set; }
        public Terrain Terrain { get; set; }
        public TerrainData TerrainData 
        {
            get => Terrain.terrainData;
            set => Terrain.terrainData = value;
        }
        public float2 Coord { get; set; }
        private Bounds Bounds { get; set; }
        private Vector2 Position { get; set; }
        private bool _isAvailable;
        readonly IPlayerInfo playerInfo = ServiceLocator.GetService<IPlayerInfo>();
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
            if (GameObject == null) return;
            _isAvailable = true;
            GameObject.SetActive(false);
        }
        // Update the status of the chunk
        public bool UpdateStatus()
        {
            
            // get the distance from the viewer to the nearest edge of the chunk
            Vector3 player =  playerInfo.PlayerPosition();
            var viewerDstFromNearestEdge = Mathf.Sqrt(Bounds.SqrDistance(new Vector2(player.x, player.z)));
            //var viewerDstFromNearestEdge = Vector2.Distance(Position, new Vector2(player.x,player.z));
            var visible = viewerDstFromNearestEdge <= ChunkConfig.maxViewDst;
            // set the visibility of the chunk
            SetVisible(visible);
            return visible;

        }
        public void SetVisible(bool visible)
        {
            if (GameObject == null) return;
            GameObject.SetActive(visible);
        }
        public bool IsVisible()
        {
            return GameObject.activeSelf;
        }
        public bool CheckDistanceAndRelease()
        {
            // if the distance from the viewer to the chunk is greater than the max view distance
            Vector3 player =  playerInfo.PlayerPosition();
            if (!(Vector2.Distance(Position, new Vector2(player.x,player.z)) >
                  ChunkConfig.maxViewDst + ChunkConfig.width * 2)) return false;
            Release();
            
            return true;
        }
    }
}