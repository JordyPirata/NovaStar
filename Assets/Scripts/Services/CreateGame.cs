using UnityEngine;
using System.IO;
using Console = UnityEngine.Debug;
using System.Threading.Tasks;


namespace Services
{
    public class CreateGame : ICreateGame
    {
        private IRepository GameRepository = ServiceLocator.GetService<IRepository>();
        private string message;

        public World CreateWorld(string GameName)
        {
            World game = new()
            {
                GameName = GameName,
                seed = Random.Range(0, int.MaxValue)
            };
            game.GameDirectory = Path.Combine(Application.persistentDataPath, game.GameName);
            game.GamePath = Path.Combine(game.GameDirectory, string.Concat(game.GameName, ".bin"));
            return game;
        }
        
        public async Task<World> UpdateWorld(World game, string newGameName)
        {
            if (!GameRepository.ExistsDirectory(game.GameDirectory))
            {
                throw new System.Exception("Game not found");
            }
            _ = GameRepository.Delete(game.GamePath);
            // Rename Directory
            Directory.Move(game.GameDirectory, Path.Combine(Application.persistentDataPath, newGameName));
            // Set the new game
            game.GameName = newGameName;
            game = UpdateDir(newGameName, game);
            // Update the game
            message = await GameRepository.Create(game, game.GamePath);
            Console.Log(message);
            return game;
        }
        private World UpdateDir(string GameName, World game)
        {
            game.GameDirectory = Path.Combine(Application.persistentDataPath, GameName);
            game.GamePath = Path.Combine(game.GameDirectory, string.Concat(GameName, ".bin"));
            return game;
        }
        public async Task<World> SaveWorld(World world)
        {
            if (GameRepository.ExistsDirectory(world.GameDirectory))
            {
                int o = IOUtil.TimesRepeatDir(Application.persistentDataPath, world.GameName);
                // Set the game name add (n) to the name an increment it
                world.GameName = string.Concat(world.GameName, "(", o, ")");
                // Set the text of the TextMeshPro component
                world = UpdateDir(world.GameName, world);
            }
            Directory.CreateDirectory(world.GameDirectory);
            message = await GameRepository.Create(world, world.GamePath);
            Console.Log(message);
            return world;
        }
        public async Task<World> LoadWorld(string directoryPath)
        {
            World game;
            string GameName = IOUtil.GetNameDirectory(directoryPath);
            string GamePath = Path.Combine(directoryPath, string.Concat(GameName, ".bin"));
            // Load the game
            (message, game) = await GameRepository.Read<World>(GamePath);
            Console.Log(message);
            return game;
        }
    }
}