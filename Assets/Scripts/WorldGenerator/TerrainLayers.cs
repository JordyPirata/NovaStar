using UnityEngine;

public class TerrainLayers : MonoBehaviour
{
    private static TerrainLayers instance;
    public static TerrainLayers Instance { get => instance;
        private set => instance = value;
    }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public TerrainLayer[] terrainLayers;
    private Terrain _terrain;

    public void SetTerrainLayers(Terrain terrain)
    {
        _terrain = terrain;
        terrainLayers = terrain.terrainData.terrainLayers;
    }
    public void SetTerrainLayer(int index, TerrainLayer terrainLayer)
    {
        terrainLayers[index] = terrainLayer;
        _terrain.terrainData.terrainLayers = terrainLayers;
    }
    public void SetTerrainLayer(int index, Texture2D texture, float tileSize, float normalScale, float metallic, float smoothness)
    {
        var terrainLayer = new TerrainLayer
        {
            diffuseTexture = texture,
            tileSize = new Vector2(tileSize, tileSize),
            normalScale = normalScale,
            metallic = metallic,
            smoothness = smoothness
        };
        SetTerrainLayer(index, terrainLayer);
    }

    public void SetTerrainLayerByPath(int index, string path, float tileSize, float normalScale, float metallic, float smoothness)
    {
        var texture = Resources.Load<Texture2D>(path);
        SetTerrainLayer(index, texture, tileSize, normalScale, metallic, smoothness);
    }
    public TerrainLayer GetTerrainLayer(int index)
    {
        return terrainLayers[index];
    }
}