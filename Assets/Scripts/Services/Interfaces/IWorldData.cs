using Models;

namespace Services.Interfaces
{
    public interface IWorldData
    {
        string GetDirectory();
        string GetName();
        string GetPath();
        int GetSeed();
        void SetSeed(int seed);
        void SetWorld(World world);
    }
}