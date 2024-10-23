using Unity.Mathematics;
using Services.Interfaces;

namespace Services.Interfaces
{
    public interface INoiseDirector
    {
        public void SetBuilder(INoiseBuilder builder);
        public object GetNoise(float2 coords);
    }
}