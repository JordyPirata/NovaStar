using System;
using UnityEngine;
using Services.Interfaces;
namespace Services.WorldGenerator
{
    public class BiomeTexturesService : MonoBehaviour, IBiomeTexturesService
    {
        [SerializeField] private BiomeTextures _BiomeTextures;

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