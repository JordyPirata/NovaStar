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
            Debug.Log("WorldData SetWorld: " + world.Name + "\n" 
            + world.Directory + "\n" + world.WorldPath + "\n" + world.seed
            + "\n" + world.humidityRange + "\n" + world.temperatureRange);
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

        public int2 GetHumidityRange()
        {
            return world.humidityRange;
        }
        public void SetHumidityRange(int2 humidityRange)
        {
            world.humidityRange = humidityRange;
        }

        public int2 GetTemperatureRange()
        {
            return world.temperatureRange;
        }
        public void SetTemperatureRange(int2 temperatureRange)
        {
            world.temperatureRange = temperatureRange;
        }

        public void UpdateWorld()
        {
            worldCRUD.UpdateWorld(world);
        }

        public void SaveWorld()
        {
            worldCRUD.SaveWorld(world);
        }

        public bool SetIsGenerated(bool isGenerated)
        {
            world.IsGenerated = isGenerated;
            return world.IsGenerated;
        }

        public bool GetIsGenerated()
        {
            return world.IsGenerated;
        }
    }
}
