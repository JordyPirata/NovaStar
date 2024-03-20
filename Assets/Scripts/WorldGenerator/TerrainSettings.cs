using UnityEngine;

namespace Generator
{
    public class TerrainSettings
    {
        public const bool allowAutoConnect = true;
        public const int groupingID = 0;
        public const int baseMapResolution = 256;
        public const int pixelError = 1;
        public const int heightmapMaximumLOD = 0;

        public static void ApplySettings(Terrain terrain)
        {
            terrain.allowAutoConnect = allowAutoConnect;
            terrain.groupingID = groupingID;
            terrain.terrainData.baseMapResolution = baseMapResolution;
            terrain.heightmapPixelError = pixelError;
            terrain.heightmapMaximumLOD = heightmapMaximumLOD;
        }
    }
}