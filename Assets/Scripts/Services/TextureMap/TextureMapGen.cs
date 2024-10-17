using Services.Interfaces;
using UnityEngine;
using Unity.Mathematics;
using Services.NoiseGenerator;
using System;

namespace Services
{
    public class TextureMapGen : ITextureMapGen
    {
        private readonly INoiseService NoiseService = ServiceLocator.GetService<INoiseService>();
        private readonly IBiomeDic BiomeService = ServiceLocator.GetService<IBiomeDic>();
        public Texture2D GenerateTextureMap(TextureMapState tetureMapState)
        {
            int2 baseTempRange = new(-10, 30);
            int2 baseHumidityRange = new(0, 400);
            // change state temperature and humidity range to 0 - 0.25
            float2 tempRange = ConvertRange(tetureMapState.temperatureRange, baseTempRange);
            float2 humidityRange = ConvertRange(tetureMapState.humidityRange, baseHumidityRange);
            
            float TAmp = math.distance(tempRange.x, tempRange.y), HAmp = math.length(humidityRange);
            float Tdist =TAmp*2 + math.distance(0, tempRange.x)* 2;
            float Hdist = HAmp  + math.distance(0, humidityRange.x);

            Texture2D texture2D = new(tetureMapState.width, tetureMapState.height)
            {
                filterMode = FilterMode.Trilinear,
                wrapMode = TextureWrapMode.Clamp
            };
            // Get reference of the state of the noise service
            NoiseServiceState noiseState = new()
            {
                kernel = Kernel.TempNoise,
                seed = tetureMapState.seed - 1,
                noiseType = NoiseType.Perlin,
                fractalType = FractalType.FBm,
                octaves = 2,
                amplitude = TAmp,
                distance = Tdist
            };
            var temperatureMap = NoiseService.GenerateNoise(tetureMapState.coords, tetureMapState.width, tetureMapState.height, noiseState);
            
            NoiseServiceState noiseState2 = new()
            {
                kernel = Kernel.HumidityNoise,
                seed = tetureMapState.seed + 1,
                noiseType = NoiseType.Perlin,
                fractalType = FractalType.FBm,
                octaves = 2,
                amplitude = HAmp,
                distance = Hdist
            };
            var moistureMap = NoiseService.GenerateNoise(tetureMapState.coords, tetureMapState.width, tetureMapState.height, noiseState2);

            for (var x = 0; x < tetureMapState.width; x++)
            {
                for (var y = 0; y < tetureMapState.height; y++)
                {
                    Color32 color = BiomeService.GetBiomeByValues(moistureMap[x, y], temperatureMap[x, y])?.color ?? new Color32(0, 0, 0, 255);
                    texture2D.SetPixel(x, y, color);
                }
            }
            return texture2D;
        }
        float2 ConvertRange(float2 value, float2 range)
        {
            value.x = Convert025(value.x, range);
            value.y = Convert025(value.y, range);
            return value;
        }
        float Convert025(float value, float2 range){
            return (float)((value - range.x) / (range.y - range.x) * 0.25);
        }
    }
}
