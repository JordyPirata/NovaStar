using UnityEngine;
using System.IO;
using Console = UnityEngine.Debug;
using System.Threading.Tasks;
using Models;
using Random = UnityEngine.Random;
using Services.Interfaces;
using Util;
using Unity.Mathematics;


namespace Services
{
    /// <summary>
    /// Create a new world or load an existing one
    /// </summary>
    public class WorldCRUD : IWorldCRUD
    {
        private readonly IRepository GameRepository = ServiceLocator.GetService<IRepository>();
        private string message;

        public World CreateWorld(string GameName)
        {
            
            World world = new()
            {
                IsGenerated = false,
                Name = GameName,
                seed = Random.Range(0, int.MaxValue),
                temperatureRange = new(-10, 30),
                humidityRange = new(0, 400)
            };
            world.Directory = Path.Combine(Application.persistentDataPath, world.Name);
            world.WorldPath = Path.Combine(world.Directory, string.Concat(world.Name, ".bin"));
            if (GameRepository.ExistsDirectory(world.Directory))
            {
                int o = IOUtil.TimesRepeatDir(Application.persistentDataPath, world.Name);
                // Set the game name add (n) to the name an increment it
                world.Name = string.Concat(world.Name, "(", o, ")");
                // Set the text of the TextMeshPro component
                world = UpdateDir(world.Name, world);
            }
            return world;
        }
        public async Task<World> ReadWorld(string directoryPath)
        {
            World game;
            string GameName = IOUtil.GetNameDirectory(directoryPath);
            string GamePath = Path.Combine(directoryPath, string.Concat(GameName, ".bin"));
            // Load the game
            (message, game) = await GameRepository.ReadAsync<World>(GamePath);
            Console.Log(message);
            return game;
        }

        public async Task UpdateWorld(World game)
        {
            if (!GameRepository.ExistsDirectory(game.Directory))
            {
                throw new System.Exception("Game not found");
            }
            message = await GameRepository.CreateAsync(game, game.WorldPath);
            Console.Log(message);
        }
        public async Task UpdateWorld(World game, string newGameName)
        {
            if (!GameRepository.ExistsDirectory(game.Directory))
            {
                throw new System.Exception("Game not found");
            }
            _ = GameRepository.Delete(game.WorldPath);
            // Rename Directory
            try
            {
                Directory.Move(game.Directory, Path.Combine(Application.persistentDataPath, newGameName));
            }
            catch (IOException e)
            {
                Console.Log(e.Message);
            }
            // Set the new game
            game.Name = newGameName;
            game = UpdateDir(newGameName, game);
            // Update the game
            message = await GameRepository.CreateAsync(game, game.WorldPath);
            Console.Log(message);
        }
        private World UpdateDir(string GameName, World game)
        {
            game.Directory = Path.Combine(Application.persistentDataPath, GameName);
            game.WorldPath = Path.Combine(game.Directory, string.Concat(GameName, ".bin"));
            return game;
        }
        public async Task SaveWorld(World world)
        {
            Directory.CreateDirectory(world.Directory);
            
            message = await GameRepository.CreateAsync(world, world.WorldPath);
            Console.Log(message);
        }
        
    }
}