using System;
using Config;
using Models;
using Services.Interfaces;
using Unity.Mathematics;
using UnityEngine;

namespace Services
{
    /// <summary>
    /// MonoState Service that holds the actual World data
    /// </summary>
    public class WorldData : MonoBehaviour, IWorldData
    {
        private IWorldCRUD worldCRUD;
        public void Awake()
        {
            worldCRUD = ServiceLocator.GetService<IWorldCRUD>();
        }
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
            worldCRUD.UpdateWorld(world, world.Name);
        }

        public int2 GetHumidityRange()
        {
            return world.humidityRange;
        }
        public void SetHumidityRange(int2 humidityRange)
        {
            world.humidityRange = humidityRange;
            worldCRUD.UpdateWorld(world, world.Name);
        }

        public int2 GetTemperatureRange()
        {
            return world.temperatureRange;
        }
        public void SetTemperatureRange(int2 temperatureRange)
        {
            world.temperatureRange = temperatureRange;
            worldCRUD.UpdateWorld(world, world.Name);
        }
    }
}
