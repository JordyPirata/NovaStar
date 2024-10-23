using System;
using Services.Interfaces;
namespace Services.WorldGenerator
{
    public class BiomeTexturesService : IBiomeTexturesService
    {
        private BiomeTextures _BiomeTextures;

        public BiomeTextures GetBiomeTextures()
        {
            return _BiomeTextures;
        }

        public void Configure(BiomeTextures biomeTextures)
        {
            _BiomeTextures = biomeTextures;
        }
    } 
}