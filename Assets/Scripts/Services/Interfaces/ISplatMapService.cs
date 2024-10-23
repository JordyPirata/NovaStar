using Unity.Mathematics;
using UnityEngine;

namespace Services.Interfaces
{
public interface ISplatMapService
{
    public Texture2D[] GenerateSplatMap(float2 chunkCoords);
}
}