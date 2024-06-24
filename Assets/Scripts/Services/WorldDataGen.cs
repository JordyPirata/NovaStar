using UnityEngine;
using System.IO;
using Console = UnityEngine.Debug;
using System.Threading.Tasks;


namespace Services
{
    /// <summary>
    /// Create a new world or load an existing one
    /// </summary>
    public class WorldDataGen : ICreateWorld
    {
        private readonly IRepository GameRepository = ServiceLocator.GetService<IRepository>();
        private string message;

        public World CreateWorld(string GameName)
        {
            World game = new()
            {
                Name = GameName,
                seed = Random.Range(0, int.MaxValue)
            };
            game.Directory = Path.Combine(Application.persistentDataPath, game.Name);
            game.WorldPath = Path.Combine(game.Directory, string.Concat(game.Name, ".bin"));
            return game;
        }
        
        public async Task<World> UpdateWorld(World game, string newGameName)
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
            message = await GameRepository.Create(game, game.WorldPath);
            Console.Log(message);
            return game;
        }
        private World UpdateDir(string GameName, World game)
        {
            game.Directory = Path.Combine(Application.persistentDataPath, GameName);
            game.WorldPath = Path.Combine(game.Directory, string.Concat(GameName, ".bin"));
            return game;
        }
        public async Task<World> SaveWorld(World world)
        {
            if (GameRepository.ExistsDirectory(world.Directory))
            {
                int o = IOUtil.TimesRepeatDir(Application.persistentDataPath, world.Name);
                // Set the game name add (n) to the name an increment it
                world.Name = string.Concat(world.Name, "(", o, ")");
                // Set the text of the TextMeshPro component
                world = UpdateDir(world.Name, world);
            }
            Directory.CreateDirectory(world.Directory);
            
            message = await GameRepository.Create(world, world.WorldPath);
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