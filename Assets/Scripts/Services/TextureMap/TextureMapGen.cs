using Services.Interfaces;
using UnityEngine;
using Unity.Mathematics;
using Config;

namespace Services
{
    public class TextureMapGen : ITextureMapGen
    {
        private readonly INoiseService NoiseService = ServiceLocator.GetService<INoiseService>();
        private readonly IBiomeDic BiomeDic = ServiceLocator.GetService<IBiomeDic>();
        public Texture2D GenerateTextureMap(float2 coords, int width, int height)
        {
            Texture2D texture2D = new(width, height)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };
            // Get reference of the state of the noise service
            NoiseServiceState state = new();
            NoiseService.SetState(state);
            state.seed = 0;
            state.noiseType = NoiseType.OpenSimplex2;
            state.fractalType = FractalType.FBm;
            state.octaves = 6;
            state.frequency = 0.001f;
            var temperatureMap = NoiseService.GenerateNoise(coords, width, height);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var color = new Color(temperatureMap[x, y], temperatureMap[x, y], temperatureMap[x, y]);
                    texture2D.SetPixel(x, y, color);
                }
            }
            texture2D.Apply();
            return texture2D;
        }
    }
}
