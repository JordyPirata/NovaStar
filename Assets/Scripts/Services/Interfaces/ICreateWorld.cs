using System.Threading.Tasks;
using Models;

namespace Services.Interfaces
{
    public interface IWorldCRUD
    {
        public World CreateWorld(string GameName);
        public Task<World> ReadWorld(string directoryPath);
        public Task UpdateWorld(World game, string newGameName);
        public Task UpdateWorld(World game);
        public Task SaveWorld(World game);
        
    }
}