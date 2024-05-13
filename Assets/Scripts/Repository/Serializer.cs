using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Repository
{
    public class Serializer
    {
        public static async Task SerializeAsync<T>(T data, string path)
        {
            string json = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(path, json);
        }

        public static async Task<T> DeserializeAsync<T>(string path)
        {
            string json = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}