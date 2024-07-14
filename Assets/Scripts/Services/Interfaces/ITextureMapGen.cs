using UnityEngine;

namespace Services
{
    public interface ITextureMapGen
    {
        Texture2D GenerateTextureMap(TextureMapState state);
    }
}
