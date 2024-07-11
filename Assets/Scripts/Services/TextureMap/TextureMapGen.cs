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
        public Texture2D GenerateTextureMap(float2 coords, int width, int height, int _seed)
        {
            Texture2D texture2D = new(width, height)
            {
                filterMode = FilterMode.Trilinear,
                wrapMode = TextureWrapMode.Clamp
            };
            // Get reference of the state of the noise service
            NoiseServiceState state = new()
            {
                kernel = Kernel.TempNoise,
                seed = _seed - 1,
                noiseType = NoiseType.Perlin,
                fractalType = FractalType.FBm,
            };
            var temperatureMap = NoiseService.GenerateNoise(coords, width, height, state);
            NoiseServiceState state2 = new()
            {
                kernel = Kernel.HumidityNoise,
                seed = _seed + 1,
                noiseType = NoiseType.OpenSimplex2,
                fractalType = FractalType.FBm,
            };
            var moistureMap = NoiseService.GenerateNoise(coords, width, height, state2);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    Color32 color = BiomeService.GetBiomeByValues(moistureMap[x, y], temperatureMap[x, y])?.color ?? new Color32(0, 0, 0, 255);
                    texture2D.SetPixel(x, y, color);
                }
            }
            return texture2D;
        }
    }
}
