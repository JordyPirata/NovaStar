using System;
using System.Threading.Tasks;
using Models;
using Unity.Mathematics;
using UnityEngine;
using Services.Interfaces;
using Services.NoiseGenerator;

namespace Services.WorldGenerator
{
public class ChunkBuilder
{
    private static readonly INoiseDirector NoiseDirector = ServiceLocator.GetService<INoiseDirector>();
    private static readonly ISplatMapService SplatMapService = ServiceLocator.GetService<ISplatMapService>();
    private float2 _ChunkCoords;
    private ChunkObject _ChunkObject;
    private GameObject ChunkGO 
    {
        get => _ChunkObject.GameObject;
        set => _ChunkObject.GameObject = value;
    }
    private Chunk Chunk
    {
        get => _ChunkObject.ChunkData;
        set => _ChunkObject.ChunkData = value;
    }
    private Terrain _Terrain
    {
        get => _ChunkObject.Terrain;
        set => _ChunkObject.Terrain = value;
    }    
    public ChunkBuilder(float2 chunkCoords)
    {
        _ChunkCoords = chunkCoords;
        _ChunkObject = ChunkPool.Instance.GetChunk(_ChunkCoords);
    }
    public void SetGameObject()
    {
        ChunkGO.transform.position = Chunk.position;
        ChunkGO.name = Chunk.ChunkName;
        ChunkGO.layer = LayerMask.NameToLayer("Terrain");
    }
    public async Task GenerateChunkData()
    {
        Chunk = await ChunkDataGenerator.Generate(_ChunkCoords);
    }

    public void SetTerrain()
    {
        TerrainCollider terrainCollider = ChunkGO.GetComponent<TerrainCollider>();
        _Terrain = TerrainSettings.ApplySettings(_Terrain, Chunk);
        terrainCollider.includeLayers = LayerMask.GetMask("Player");
        terrainCollider.terrainData = _Terrain.terrainData;
    }
    
    public void CalculateBiomes()
    {
        
        
        var textures = SplatMapService.GenerateSplatMap(_ChunkCoords, Chunk.temperature, Chunk.humidity); 
        Debug.Log(textures.Length);
        // Calculate biome and create splatmap for terrain
        var terrainMaterial = new Material(Shader.Find("Standard"));
        // Set terrain data 

    }
    public ChunkObject GetChunkObject()
    {
        return _ChunkObject;
    }
}
}
