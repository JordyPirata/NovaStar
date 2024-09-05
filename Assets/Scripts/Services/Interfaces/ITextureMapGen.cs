using UnityEngine;

namespace Services.Interfaces
{
    public interface ITextureMapGen
    {
        Texture2D GenerateTextureMap(TextureMapState state);
    }
}
