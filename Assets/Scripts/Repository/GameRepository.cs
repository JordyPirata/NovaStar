using System.IO;
using System.Threading.Tasks;

namespace Repository
{
    public class GameRepository
    {
        // Make JsonRepository a singleton
        private static GameRepository instance;
        public static GameRepository Instance
        {
            get
            {
                instance ??= new GameRepository();
                return instance;
            }
        }
        // The CreateAsync method serializes the data and saves it to a file
        public async Task<string> Create<T>(T data, string path)
        {
            await Serializer.Instance.BSerialize(data, path);
            return "Data saved successfully on: " + path;
        }
        // The ReadAsync method deserializes the data from a file
        public async Task<(string, T)> Read<T>(string path)
        {
            T data = await Serializer.Instance.BDeserialize<T>(path);
            if (data == null)
            {
                return ("Data not found", default);
            }
            else
            {
                return ("Data read successfully", data);
            }
        }
        public async Task<string> Update<T>(T data, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                await Serializer.Instance.BSerialize(data, path);
                return "Data updated successfully";
            }
            else
            {
                return "Data not found";
            }
        }
        // The Delete method deletes a file
        public string Delete(string path)
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