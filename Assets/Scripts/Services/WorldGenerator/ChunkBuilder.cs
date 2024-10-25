using System;
using System.Threading.Tasks;
using Models;
using Unity.Mathematics;
using UnityEngine;
using Services.Interfaces;
using Services.NoiseGenerator;
using System.Collections.Generic;

namespace Services.WorldGenerator
{
public class ChunkBuilder
{
    private static readonly ISplatMapService SplatMapService = ServiceLocator.GetService<ISplatMapService>();
    public  static readonly IBiomeTexturesService TexturesService = ServiceLocator.GetService<IBiomeTexturesService>();
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
        const int tilling = 20;   
        var splatMap = SplatMapService.GenerateSplatMap(_ChunkCoords, Chunk.temperatures, Chunk.humidity); 
        // Calculate biome and create splatmap for terrain
        List<BiomeTexture> biomeTexture = TexturesService.GetBiomeTextures().Textures;
        var terrainMaterial = new Material(Shader.Find("Custom/TerrainShader"));

        terrainMaterial.SetTexture("_SplatMap1", splatMap[0]);
        terrainMaterial.SetTexture("_SplatMap2", splatMap[1]);

        terrainMaterial.SetTexture("_TundraAlbedo", biomeTexture[0].Albedo);
        terrainMaterial.SetTexture("_TundraHeight", biomeTexture[0].Height);
        terrainMaterial.SetTexture("_TundraNormal", biomeTexture[0].Normal);
        terrainMaterial.SetTextureScale("_TundraAlbedo", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_TundraHeight", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_TundraNormal", new Vector2(tilling, tilling));
        
        terrainMaterial.SetTexture("_TaigaAlbedo", biomeTexture[1].Albedo);
        terrainMaterial.SetTexture("_TaigaHeight", biomeTexture[1].Height);
        terrainMaterial.SetTexture("_TaigaNormal", biomeTexture[1].Normal);
        terrainMaterial.SetTextureScale("_TaigaAlbedo", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_TaigaHeight", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_TaigaNormal", new Vector2(tilling, tilling));

        terrainMaterial.SetTexture("_DessertAlbedo", biomeTexture[2].Albedo);
        terrainMaterial.SetTexture("_DessertHeight", biomeTexture[2].Height);
        terrainMaterial.SetTexture("_DessertNormal", biomeTexture[2].Normal);
        terrainMaterial.SetTextureScale("_DessertAlbedo", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_DessertHeight", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_DessertNormal", new Vector2(tilling, tilling));

        terrainMaterial.SetTexture("_ForestAlbedo", biomeTexture[3].Albedo);
        terrainMaterial.SetTexture("_ForestHeight", biomeTexture[3].Height);
        terrainMaterial.SetTexture("_ForestNormal", biomeTexture[3].Normal);
        terrainMaterial.SetTextureScale("_ForestAlbedo", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_ForestHeight", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_ForestNormal", new Vector2(tilling, tilling));

        terrainMaterial.SetTexture("_JungleAlbedo", biomeTexture[4].Albedo);
        terrainMaterial.SetTexture("_JungleHeight", biomeTexture[4].Height);
        terrainMaterial.SetTexture("_JungleNormal", biomeTexture[4].Normal);
        terrainMaterial.SetTextureScale("_JungleAlbedo", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_JungleHeight", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_JungleNormal", new Vector2(tilling, tilling));

        terrainMaterial.SetTexture("_SavannaAlbedo", biomeTexture[5].Albedo);
        terrainMaterial.SetTexture("_SavannaHeight", biomeTexture[5].Height);
        terrainMaterial.SetTexture("_SavannaNormal", biomeTexture[5].Normal);
        terrainMaterial.SetTextureScale("_SavannaAlbedo", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_SavannaHeight", new Vector2(tilling, tilling));
        terrainMaterial.SetTextureScale("_SavannaNormal", new Vector2(tilling, tilling));

        _Terrain.materialTemplate = terrainMaterial;
        // Set terrain data 

    }
    public ChunkObject GetChunkObject()
    {
        return _ChunkObject;
    }
}
}
