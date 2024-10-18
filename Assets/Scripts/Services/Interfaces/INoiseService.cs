using Unity.Mathematics;
using Services.Interfaces;

namespace Services.Interfaces
{
    public interface INoiseDirector
    {
        public void SetBuilder<T>(T builder);
        public object MakeNoise(float2 coords);
    }
}