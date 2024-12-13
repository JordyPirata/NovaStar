using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRepository
    {
        string Create<T>(T data, string path);
        Task<string> CreateAsync<T>(T data, string path);
        (string, T) Read<T>(string path);
        Task<(string, T)> ReadAsync<T>(string path);
        string Delete(string path);
        bool ExistsFile(string path);
        bool ExistsDirectory(string path);
        public Task<string> CreateJson<T>(T data, string path);
        public Task<(string, T)> ReadJson<T>(string path);
    }
}