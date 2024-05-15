using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Repository
{
    public class Serializer
    {
        // Make Serializer a singleton
        private static Serializer instance;
        public static Serializer Instance
        {
            get
            {
                instance ??= new Serializer();
                return instance;
            }
        }
        // Serialize the data to a binary file
        private readonly BinaryFormatter binaryFormatter = new();
        public void BSerialize<T>(T data, string path)
        {
            using Stream stream = File.Open(path, FileMode.Create);
            binaryFormatter.Serialize(stream, data);
        }
        // Deserialize the data from a binary file
        public T BDeserialize<T>(string path)
        {
            using Stream stream = File.Open(path, FileMode.Open);
            return (T)binaryFormatter.Deserialize(stream);
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