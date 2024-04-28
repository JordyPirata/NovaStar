using UnityEngine;
using Util;

namespace Generator
{
    public class TerrainSettings
    {
        public const bool allowAutoConnect = true;
        public const int groupingID = 0;
        public const int baseMapResolution = 4096;
        public const int pixelError = 5;
        public const int heightmapMaximumLOD = 0;

        public static Terrain ApplySettings(Terrain terrain, Chunk chunk)
        {

            terrain.allowAutoConnect = allowAutoConnect;
            terrain.groupingID = groupingID;
            terrain.heightmapPixelError = pixelError;
            terrain.heightmapMaximumLOD = heightmapMaximumLOD;

            TerrainData terrainData = new()
            {
                baseMapResolution = baseMapResolution,
                heightmapResolution = chunk.width,
                size = new Vector3(chunk.width, chunk.height, chunk.width)
            };

            terrainData.SetHeights(0, 0,TransferData.TransferDataFromArrayTo2DArray(chunk.heights,chunk.width,chunk.depth));
            terrain.terrainData = terrainData;
            
            return terrain;
        }
    }
}