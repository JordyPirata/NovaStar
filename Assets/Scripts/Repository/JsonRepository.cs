using System.Threading.Tasks;
using System.IO;

namespace Repository
{
    public class JsonRepository
    {
        // The serializer is a dependency of the JsonRepository
        private readonly Serializer serializer = new();
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
            try
            {
                await serializer.SerializeAsync(data, path);
                return "Data saved successfully on: " + path;
            }
            catch (System.Exception e)
            {
                return e.Message;
            }
        }
        // The ReadAsync method deserializes the data from a file
        public async Task<(string, T)> ReadAsync<T>(string path)
        {
            try
            {
                T data = await serializer.DeserializeAsync<T>(path);
                if (data == null)
                {
                    return ("Data not found", default);
                }
                else
                {
                    return ("Data read successfully", data);
                }
            }
            catch (System.Exception e)
            {
                return (e.Message, default);
            }
        }
        // The Delete method deletes a file
        public string Delete(string path)
        {
            try
            {
                File.Delete(path);
                return "File deleted successfully";
            }
            catch (System.Exception e)
            {
                return e.Message;
            }
        }
        public bool Exist(string path)
        {
            return File.Exists(path);
        }
    }
}