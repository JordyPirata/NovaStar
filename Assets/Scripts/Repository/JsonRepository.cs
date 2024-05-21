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
        public string Create<T>(T data, string path)
        {
            Serializer.Instance.BSerialize(data, path);
            return "Data saved successfully on: " + path;
        }
        // The ReadAsync method deserializes the data from a file
        public (string, T) Read<T>(string path)
        {
            T data = Serializer.Instance.BDeserialize<T>(path);
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