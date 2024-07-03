using System;
using Models.Biomes;

namespace Services.Interfaces
{
    public interface IBiomeDic
    {
        Biome GetBiome(Type type);
        Biome GetBiomeByValues(float humidity, float temperature);
    }
}