using UnityEngine;
using Unity.Mathematics;

namespace Services
{
    public interface ITextureMapGen
    {
        Texture2D GenerateTextureMap(float2 coords, int width, int height, int seed);
    }
}
