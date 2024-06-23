using System.Threading.Tasks;

namespace Services
{
    public interface ICreateGame
    {
        public World CreateWorld(string GameName);
        public Task<World> UpdateWorld(World game, string newGameName);
        public Task<World> SaveWorld(World game);
        public Task<World> LoadWorld(string directoryPath);
    }
}