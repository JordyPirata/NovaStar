using System.Threading.Tasks;
public interface IRepository
{
    Task<string> Create<T>(T data, string path);
    Task<(string, T)> Read<T>(string path);
    string Delete(string path);
    bool ExistsFile(string path);
    string DeleteDirectory(string path);
    bool ExistsDirectory(string path);
}