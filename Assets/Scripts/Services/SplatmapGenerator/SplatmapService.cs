using Services.NoiseGenerator;
using Services.Interfaces;
using Unity.Mathematics;
using UnityEngine;
using Models.Biomes;

namespace Services.Splatmap
{

public class SplatMapService : ISplatMapService 
{
    private IBiomeDic BiomeDic => ServiceLocator.GetService<IBiomeDic>();
    private ComputeShader SplatMapShader => Resources.Load<ComputeShader>("SplatMapShader");
    // Shader Properties
    private int Kernel => SplatMapShader.FindKernel("CSMain");
    private int SplatMap1Id => Shader.PropertyToID("SplatMap1");
    private int SplatMap2Id => Shader.PropertyToID("SplatMap2");
    private int BiomeMapId => Shader.PropertyToID("BiomeMap");

    public Texture2D[] GenerateSplatMap(float2 chunkCoords, float[] tempNoise, float[] humiditynoise)
    {
        int[] BiomeArray = new int[tempNoise.Length];

        for (int i = 0; i < humiditynoise.Length; i++)
        {
            Biome biome = BiomeDic.GetBiomeByValues(humiditynoise[i], tempNoise[i]);
            switch (biome)
            {
                case Desert:
                    BiomeArray[i] = 0;
                    break; 
                case Forest:
                    BiomeArray[i] = 1;
                    break;
                case Jungle:
                    BiomeArray[i] = 2;
                    break;
                case Savanna:
                    BiomeArray[i] = 3;
                    break;
                case Taiga:
                    BiomeArray[i] = 4;
                    break;
                case Tundra:
                    BiomeArray[i] = 5;
                    break;
                default:
                    break;
            }
        }
        var biomeBuffer = new ComputeBuffer(BiomeArray.Length, sizeof(int));
        biomeBuffer.SetData(BiomeArray);

        var splatMap1 = new Texture2D(257, 257, TextureFormat.RFloat, false);
        var splatMap2 = new Texture2D(257, 257, TextureFormat.RFloat, false);
        
        SplatMapShader.SetBuffer(Kernel, BiomeMapId, biomeBuffer);

        SplatMapShader.SetTexture(Kernel, SplatMap1Id, splatMap1);
        SplatMapShader.SetTexture(Kernel, SplatMap2Id, splatMap2);

        SplatMapShader.Dispatch(Kernel, 257, 1, 1);
        
        biomeBuffer.Release();

        return new Texture2D[] {splatMap1, splatMap2};

    }
}
}