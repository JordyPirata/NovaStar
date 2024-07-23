using Config;
using Models;
using Services.Interfaces;
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
            Debug.Log("WorldData SetWorld: " + world.Name);
        }
        public string GetDirectory()
        {
            return world.Directory;
        }
        public string GetName()
        {
            return world.Name;
        }
        public string GetPath()
        {
            return world.WorldPath;
        }
        public int GetSeed()
        {
            return world.seed;
        }

        public void SetSeed(int seed)
        {
            world.seed = seed;
        }
    }
}
