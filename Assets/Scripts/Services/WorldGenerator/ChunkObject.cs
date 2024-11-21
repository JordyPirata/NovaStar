using System;
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
            Bounds = new(Position, Vector2.one * ChunkConfig.width); // original is less than 2
            _isAvailable = false;
            return this;
        }
        /// <summary>
        /// Get the height of the terrain at the given local coordinates
        /// </summary>
        /// <returns>float height value</returns>
        public float GetHeight(int x, int y)
        {
            return Terrain.terrainData.GetHeight(x, y);
        }
        /// <summary>
        /// Get the temperature of the terrain at the given local coordinates
        /// </summary>
        /// <returns>float temperature value</returns>Time.
        public float GetTemperature(int x, int y)
        {
            int X = (int)(Coord.x * ChunkConfig.width) - x;
            int Y = (int)(Coord.y * ChunkConfig.width) - y;
            Debug.Log($"x:{x} y:{y} X: {X} Y: {Y} position: {Coord}");
            var i = Util.TransferData.GetIndex(X, Y, ChunkConfig.width);
            return ChunkData.temperatures[i];
        }
        /// <summary>
        /// Get the humidity of the terrain at the given local coordinates
        /// </summary>
        /// <returns>float humidity value</returns>
        public float GetHumidity(int x, int y)
        {
            // Get local coordinates
            int X = (int)Coord.x - x;
            int Y = (int)Coord.y - y;
            var i = Util.TransferData.GetIndex(X, Y, ChunkConfig.width);
            return ChunkData.humidity[i];
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