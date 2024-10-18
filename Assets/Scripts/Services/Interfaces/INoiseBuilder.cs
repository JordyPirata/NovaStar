using Unity.Mathematics;
namespace Services.Interfaces
{
    public interface INoiseBuilder
    {
        public void SetSize(int width, int depth);
        public void SetCoords(float2 coords);
        public void SetState(NoiseState state);
        public void SetKernel();
        public object GetNoise();
        public void Build();
    }
}
