using UnityEngine;

namespace Services
{
/// <summary>
/// MonoState Service that holds the actual World data
/// </summary>
public class WorldData : MonoBehaviour, IWorldData
{
    private static World world;
    public void SetWorld(World world)
    {
        WorldData.world = world;
    }
    public string GetWorldDirectory()
    {
        return world.Directory;   
    }
    public string GetWorldName()
    {
        return world.Name;
    }
    public string GetWorldPath()
    {
        return world.WorldPath;
    }
    public int GetWorldSeed()
    {
        return world.seed;
    }
}}
internal interface IWorldData
{
    string GetWorldDirectory();
    string GetWorldName();
    string GetWorldPath();
    int GetWorldSeed();
    void SetWorld(World world);
}