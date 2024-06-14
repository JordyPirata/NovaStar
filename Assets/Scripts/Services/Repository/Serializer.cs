using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Services.Repository
{
    public class Serializer
    {
        // Serialize the data to a binary file
        private static readonly BinaryFormatter binaryFormatter = new();
        public static async Task BSerialize<T>(T data, string path)
        {
            using Stream stream = File.Open(path, FileMode.Create);
            await Task.Run(() => binaryFormatter.Serialize(stream, data));
        }
        // Deserialize the data from a binary file
        public static async Task<T> BDeserialize<T>(string path)
        {
            using Stream stream = File.Open(path, FileMode.Open);
            return await Task.Run( () =>(T)binaryFormatter.Deserialize(stream));
        }
        [System.Obsolete]
        public static async Task SerializeAsync<T>(T data, string path)
        {
            string json = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(path, json);
        } 
        [System.Obsolete]
        public static async Task<T> DeserializeAsync<T>(string path)
        {
            string json = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}