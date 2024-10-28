using Services.NoiseGenerator;
using Services.Interfaces;
using Unity.Mathematics;
using UnityEngine;
using Models.Biomes;
using System.IO;

namespace Services.Splatmap
{

public class SplatMapService : ISplatMapService 
{
    private IBiomeDic BiomeDic => ServiceLocator.GetService<IBiomeDic>();
    private ComputeShader SplatMapShader => Resources.Load<ComputeShader>(Path.Combine("Shaders", "SplatMapShader"));
    // Shader Properties
    private int Kernel => SplatMapShader.FindKernel("CSMain");
    private int SplatMap1Id => Shader.PropertyToID("SplatMap1");
    private int SplatMap2Id => Shader.PropertyToID("SplatMap2");
    private int BiomeMapId => Shader.PropertyToID("BiomeMap");

    public RenderTexture[] GenerateSplatMap(float2 chunkCoords, float[] tempNoise, float[] humiditynoise)
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

        var splatMap1 = new RenderTexture(258, 258, 1)
        {
            enableRandomWrite = true,
            filterMode = FilterMode.Point
        };
        splatMap1.Create();

        var splatMap2 = new RenderTexture(258, 258, 1)
        {
            enableRandomWrite = true,
            filterMode = FilterMode.Point
        };
        splatMap2.Create();

        SplatMapShader.SetBuffer(Kernel, BiomeMapId, biomeBuffer);

        SplatMapShader.SetTexture(Kernel, SplatMap1Id, splatMap1);
        SplatMapShader.SetTexture(Kernel, SplatMap2Id, splatMap2);

        SplatMapShader.Dispatch(Kernel, 258/6 ,258/6, 1);
        
        biomeBuffer.Release();

        return new RenderTexture[] {splatMap1, splatMap2};

    }
}
}