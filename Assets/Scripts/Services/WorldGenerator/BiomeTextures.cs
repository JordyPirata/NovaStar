using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;
namespace Services.WorldGenerator
{
    [CreateAssetMenu(fileName = "TextureReference", menuName = "NovaStar/TextureReference")]
    public class BiomeTextures : ScriptableObject
    {
        public List<BiomeTexture> Textures;
    }
}