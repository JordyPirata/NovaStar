public interface IWorldData
{
    string GetWorldDirectory();
    string GetWorldName();
    string GetWorldPath();
    int GetWorldSeed();
    void SetWorld(World world);
}