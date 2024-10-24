using Unity.Mathematics;
using UnityEngine;

namespace Services.Interfaces
{
public interface ISplatMapService
{
    public RenderTexture[] GenerateSplatMap(float2 chunkCoords, float[] tempNoise, float[] humiditynoise);
}
}