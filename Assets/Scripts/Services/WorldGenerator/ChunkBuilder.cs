using System;
using System.Threading.Tasks;
using Models;
using Unity.Mathematics;
using UnityEngine;
using Services.Interfaces;

namespace Services.WorldGenerator
{
public class ChunkBuilder
{
    private float2 _ChunkCoords;
    private ChunkObject _ChunkObject;
    private GameObject ChunkGO 
    {
        get => _ChunkObject.GameObject;
        set => _ChunkObject.GameObject = value;
    }
    private Chunk _Chunk
    {
        get => _ChunkObject.ChunkData;
        set => _ChunkObject.ChunkData = value;
    }    
    public ChunkBuilder(float2 chunkCoords)
    {
        _ChunkCoords = chunkCoords;
        _ChunkObject = ChunkPool.Instance.GetChunk(_ChunkCoords);
    }
    public void SetGameObject()
    {
        ChunkGO.transform.position = _Chunk.position;
        ChunkGO.name = _Chunk.ChunkName;
        ChunkGO.layer = LayerMask.NameToLayer("Terrain");
    }
    public async Task GenerateChunkData()
    {
        _ChunkObject.ChunkData = await ChunkDataGenerator.Generate(_ChunkCoords);
    }

    public void SetTerrain()
    {
        TerrainCollider terrainCollider = ChunkGO.GetComponent<TerrainCollider>();
        Terrain terrain = ChunkGO.GetComponent<Terrain>();
        terrain = TerrainSettings.ApplySettings(terrain, _Chunk);
        terrainCollider.includeLayers = LayerMask.GetMask("Player");
        terrainCollider.terrainData = terrain.terrainData;
    }
    
    public void CalculateBiomes()
    {

        ISplatMapService splatMapService = ServiceLocator.GetService<ISplatMapService>();
        var textures = splatMapService.GenerateSplatMap(_ChunkCoords);
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
