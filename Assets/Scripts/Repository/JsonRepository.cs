using System.Threading.Tasks;
using System.IO;

namespace Repository
{
    public class JsonRepository
    {
        // Make JsonRepository a singleton
        private static JsonRepository instance;
        public static JsonRepository Instance
        {
            get
            {
                instance ??= new JsonRepository();
                return instance;
            }
        }
        // The CreateAsync method serializes the data and saves it to a file
        public async Task<string> CreateAsync<T>(T data, string path)
        {
            await Serializer.SerializeAsync(data, path);
            return "Data saved successfully on: " + path;
        }
        // The ReadAsync method deserializes the data from a file
        public async Task<(string, T)> ReadAsync<T>(string path)
        {
            T data = await Serializer.DeserializeAsync<T>(path);
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
        public string Delete(string path)
        {
            File.Delete(path);
            return "File deleted successfully";
        }
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}