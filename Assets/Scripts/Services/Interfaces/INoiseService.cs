using Unity.Mathematics;
using Services.Interfaces;

namespace Services.Interfaces
{
    public interface INoiseDirector
    {
        public void SetBuilder(INoiseBuilder builder);
        public object MakeNoise(float2 coords);
    }
}