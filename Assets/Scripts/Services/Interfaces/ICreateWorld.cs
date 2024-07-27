using System.Threading.Tasks;
using Models;

namespace Services.Interfaces
{
    public interface IWorldCRUD
    {
        public World CreateWorld(string GameName);
        public Task<World> UpdateWorld(World game, string newGameName);
        public Task<World> UpdateWorld(World game);
        public Task<World> SaveWorld(World game);
        public Task<World> LoadWorld(string directoryPath);
    }
}