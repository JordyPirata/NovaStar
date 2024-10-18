using Unity.Mathematics;
namespace Services.Interfaces
{
    public interface INoiseBuilder<T>
    {
        public void SetSize(int width, int depth);
        public void SetCoords(float2 coords);
        public void SetState(NoiseState state);
        public void SetKernel();
        public T GetNoise();
        public void Build();
    }
}
