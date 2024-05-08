using UnityEngine;
using Util;

namespace Generator
{
    public class TerrainSettings
    {
        public const bool allowAutoConnect = true;
        public const int groupingID = 0;
        public const int pixelError = 1;
        public const int heightmapMaximumLOD = 0;

        public static Terrain ApplySettings(Terrain terrain, Chunk chunk)
        {
            terrain.materialTemplate = ChunkManager.Instance.material;
            terrain.allowAutoConnect = allowAutoConnect;
            terrain.groupingID = groupingID;
            terrain.heightmapPixelError = pixelError;
            terrain.heightmapMaximumLOD = heightmapMaximumLOD;

            TerrainData terrainData = new()
            {
                heightmapResolution = chunk.width,
                size = new Vector3(chunk.width, chunk.height, chunk.width)
            };
            // TODO: ROTATE THE HEIGHTS
            terrainData.SetHeights(0, 0,TransferData.TransferDataFromArrayTo2DArray(chunk.heights,chunk.width,chunk.depth));
            terrain.terrainData = terrainData;
            
            return terrain;
        }
    }
}