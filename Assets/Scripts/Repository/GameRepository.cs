using System.IO;
using System.Threading.Tasks;

namespace Repository
{
    public class GameRepository
    {
        // The CreateAsync method serializes the data and saves it to a file
        public static async Task<string> Create<T>(T data, string path)
        {
            await Serializer.BSerialize(data, path);
            return "Data saved successfully on: " + path;
        }
        // The ReadAsync method deserializes the data from a file
        public static async Task<(string, T)> Read<T>(string path)
        {
            T data = await Serializer.BDeserialize<T>(path);
            if (data == null)
            {
                return ("Data not found", default);
            }
            else
            {
                return ("Data read successfully", data);
            }
        }
        // The Delete method deletes a file
        public static string Delete(string path)
        {
            File.Delete(path);
            return "File deleted successfully";
        }
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }
        public static string DeleteDirectory(string path)
        {
            Directory.Delete(path, true);
            return "Directory deleted successfully";
        }
        public static bool ExistsDirectory(string path)
        {
            return Directory.Exists(path);
        }
    }
}