using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRepository
    {
        Task<string> Create<T>(T data, string path);
        Task<(string, T)> Read<T>(string path);
        string Delete(string path);
        bool ExistsFile(string path);
        bool ExistsDirectory(string path);
        public Task<string> CreateJson<T>(T data, string path);
        public Task<(string, T)> ReadJson<T>(string path);
    }
}