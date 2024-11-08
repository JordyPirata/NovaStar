using Services.Interfaces;
using UnityEngine;
using Unity.Mathematics;
using Services.NoiseGenerator;
using System;
using Util;

namespace Services
{
    public class TextureMapGen : ITextureMapGen
    {
        public const int Width = 200;
        public const int Depth = 200;
        private readonly INoiseDirector NoiseDirector = ServiceLocator.GetService<INoiseDirector>();
        private readonly IBiomeDic BiomeService = ServiceLocator.GetService<IBiomeDic>();
        public Texture2D GenerateTextureMap(TextureMapState tetureMapState)
        {
            Texture2D texture2D = new(Width, Depth)
            {
                filterMode = FilterMode.Trilinear,
                wrapMode = TextureWrapMode.Clamp
            };

            // Get the amplitude and distance for the temperature noise

            (float TAmp, float Tdist) = NoiseRange.GetTemperatureAmpDist(tetureMapState.temperatureRange);
            
            // Get reference of the state of the noise service
            NoiseState temperature = new()
            {
                seed = tetureMapState.seed - 1,
                noiseType = NoiseType.Perlin,
                fractalType = FractalType.FBM,
                frequency = 0.01f,
                octaves = 1,
                amplitude = TAmp,
                distance = Tdist
            };
            NoiseDirector.SetBuilder(new TempNoiseBuilder());
            NoiseDirector.SetExternalState(temperature);
            var temperatureMap = NoiseDirector.GetNoise(new float2(0,0)) as float[,];
            
            // Get the amplitude and distance for the humidity noise
            (float HAmp, float Hdist) = NoiseRange.GetHumidityAmpDist(tetureMapState.humidityRange);

            NoiseState noiseState2 = new()
            {
                seed = tetureMapState.seed + 1,
                noiseType = NoiseType.Perlin,
                fractalType = FractalType.FBM,
                frequency = 0.01f,
                octaves = 1,
                amplitude = HAmp,
                distance = Hdist
            };
            NoiseDirector.SetBuilder(new HumidityNoiseBuilder());
            NoiseDirector.SetExternalState(noiseState2);
            var moistureMap = NoiseDirector.GetNoise(new float2(0,0)) as float[,];

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Depth; y++)
                {
                    Color32 color = BiomeService.GetBiomeByValues(moistureMap[x, y], temperatureMap[x, y])?.color ?? new Color32(0, 0, 0, 255);
                    texture2D.SetPixel(x, y, color);
                }
            }
            return texture2D;
        }
    }
}
