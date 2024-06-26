using Unity.Burst;
using UnityEngine;
using Util;
using UnityEngine.Rendering;
namespace WorldGenerator
{
    [BurstCompile]
    public struct TerrainSettings
    {
        public const bool allowAutoConnect = true;
        public const int groupingID = 0;
        public const int pixelError = 0;
        public const int heightmapMaximumLOD = 0;
        public const int basemapDistance = 257;
        private static Material DefaultTerrainMaterial { get{return GetDefaultTerrainMaterial();}}

        public static Terrain ApplySettings(Terrain terrain, Chunk chunk)
        {
            terrain.materialTemplate = DefaultTerrainMaterial;
            terrain.allowAutoConnect = allowAutoConnect;
            terrain.groupingID = groupingID;
            terrain.heightmapPixelError = pixelError;
            terrain.heightmapMaximumLOD = heightmapMaximumLOD;
            terrain.basemapDistance = basemapDistance; 

            TerrainData terrainData = new()
            {
                terrainLayers = TerrainLayers.Instance.terrainLayers,
                heightmapResolution = chunk.width,
                size = new Vector3(chunk.width, chunk.height, chunk.width)
            };
            terrainData.SetHeights(0, 0,TransferData.TransferDataFromArrayTo2DArray(chunk.heights,chunk.width,chunk.depth));
            terrain.terrainData = terrainData;
            
            return terrain;
        }
        public static Material GetDefaultTerrainMaterial ()
        {

			return Resources.Load<Material>("Ground1");
            
		}
    }
}