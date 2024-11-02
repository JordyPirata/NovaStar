using System;
using System.Collections.Generic;
using Models.Biomes;
using Unity.Mathematics;
using UnityEngine;
using Services.Interfaces;

namespace Services
{
/// <summary>
///  This class is a dictionary of biomes that can be used to get a biome by its type or by its humidity and temperature values.
/// </summary>
public class BiomesDic : IBiomeDic
{
    private static Dictionary<Type, Biome> Biomes;
    private static void InitializeBiomes()
    {
        Biomes = new Dictionary<Type, Biome>
        {
            // Add your biomes and their corresponding values here
            [typeof(Tundra)] = new Tundra()
            {
                humidityRange = new float2(0, 1),
                temperatureRange = new float2(0.0f, 0.25f),
                color = new Color32(212, 219, 206, 255) // #d4dbce light grey
            },
            [typeof(Taiga)] = new Taiga()
            {
                humidityRange = new float2(0.25f, 1f),
                temperatureRange = new float2(0.25f, 0.4f),
                color = new Color32(63, 133, 13, 255) // #3f850d dark green
            },
            [typeof(Desert)] = new Desert()
            {
                humidityRange = new float2(0.0f, 0.25f),
                temperatureRange = new float2(0.25f, 0.55f),
                color = new Color32(204, 204, 135, 255) // #cccc87 sand
            },
            [typeof(Forest)] = new Forest()
            {
                humidityRange = new float2(0.25f, 1),
                temperatureRange = new float2(0.4f, 0.55f),
                color = new Color32(34, 139, 34, 255) // #228b22 forest green
            },
            [typeof(Jungle)] = new Jungle()
            {
                humidityRange = new float2(0.5f, 1),
                temperatureRange = new float2(0.55f, 1.0f),
                color = new Color32(0, 100, 0, 255) // #006400 dark green
            },
            [typeof(Savanna)] = new Savanna()
            {
                humidityRange = new float2(0.0f, 0.5f),
                temperatureRange = new float2(0.55f, 1.0f),
                color = new Color32(218, 165, 32, 255) // #daa520 gold
            },
        };
    }
    public Biome GetBiome(Type type)
    {
        if (Biomes == null) InitializeBiomes();
        return Biomes!.TryGetValue(type, out var biome)? biome : null;
        
    }
    public Biome GetBiomeByValues(float humidity, float temperature)
    {
        if (Biomes == null) InitializeBiomes();
        
        foreach (var biome in Biomes!.Values)
        {
            if (humidity >= biome.humidityRange.x && humidity <= biome.humidityRange.y &&
                temperature >= biome.temperatureRange.x && temperature <= biome.temperatureRange.y)
            {
                return biome;
            }
        }
        return null;
    }
    
}
}