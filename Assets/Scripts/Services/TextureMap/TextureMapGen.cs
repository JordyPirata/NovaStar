using Services.Interfaces;
using UnityEngine;
using Unity.Mathematics;
using Services.NoiseGenerator;

namespace Services
{
    public class TextureMapGen : ITextureMapGen
    {
        private readonly INoiseService NoiseService = ServiceLocator.GetService<INoiseService>();
        private readonly IBiomeDic BiomeService = ServiceLocator.GetService<IBiomeDic>();
        public Texture2D GenerateTextureMap(TextureMapState state)
        {
            int2 baseTempRange = new(-10, 30);
            int2 baseHumidityRange = new(0, 400);
            // change state temperature range into range between 0 and .25f
            float tempRange = ConvertRange(state.temperatureRange.x, baseTempRange.x, baseTempRange.y, 0, .25f);
            float humRange = math.length(baseHumidityRange);
            
            float TAmp = .25f, HAmp = .25f;
            float Tdist = .5f, Hdist = .5f;
            Texture2D texture2D = new(state.width, state.height)
            {
                filterMode = FilterMode.Trilinear,
                wrapMode = TextureWrapMode.Clamp
            };
            // Get reference of the state of the noise service
            NoiseServiceState noiseState = new()
            {
                kernel = Kernel.TempNoise,
                seed = state.seed - 1,
                noiseType = NoiseType.Perlin,
                fractalType = FractalType.FBm,
                amplitude = TAmp,
                distance = Tdist
            };
            var temperatureMap = NoiseService.GenerateNoise(state.coords, state.width, state.height, noiseState);
            NoiseServiceState noiseState2 = new()
            {
                kernel = Kernel.HumidityNoise,
                seed = state.seed + 1,
                noiseType = NoiseType.OpenSimplex2,
                fractalType = FractalType.FBm,
                amplitude = HAmp,
                distance = Hdist
            };
            var moistureMap = NoiseService.GenerateNoise(state.coords, state.width, state.height, noiseState2);

            for (var x = 0; x < state.width; x++)
            {
                for (var y = 0; y < state.height; y++)
                {
                    Color32 color = BiomeService.GetBiomeByValues(moistureMap[x, y], temperatureMap[x, y])?.color ?? new Color32(0, 0, 0, 255);
                    texture2D.SetPixel(x, y, color);
                }
            }
            return texture2D;
        }
        (float, float) ConvertRange(float value, float2 range)
        {
            float amplitude = (float)((value - range.x) / (range.y - range.x) * 0.25);
            return (amplitude, math.length(range));
        }
    }
}
