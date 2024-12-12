using System.IO;
using System.Threading.Tasks;
using Services.Interfaces;

namespace Services.Repository
{
    //TODO: Create a directory Manager to handle the dinamic creation of directories
    public class GameRepository : IRepository
    {
        // The CreateAsync method serializes the data and saves it to a file
        public async Task<string> Create<T>(T data, string path)
        {
            await Serializer.BSerialize(data, path);
            return "Data saved successfully on: " + path;
        }
        
        public async Task<string> CreateJson<T>(T data, string path)
        {
            await Serializer.JsonSerialize(data, path);
            return "Data saved successfully on: " + path;
        }
        // The ReadAsync method deserializes the data from a file
        public async Task<(string, T)> Read<T>(string path)
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
        
        public async Task<(string, T)> ReadJson<T>(string path)
        {
            T data = await Serializer.JsonDeserialize<T>(path);
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
            // Check if path is a file or directory with FileAttributes
            FileAttributes attr = File.GetAttributes(path);
            switch (attr)
            {
                case FileAttributes.Directory:
                    Directory.Delete(path, true);
                    return "Directory deleted successfully";
                case FileAttributes.Archive:
                    File.Delete(path);
                    return "File deleted successfully";
                default:
                    return "Path not found";
            }
        }
        // The Exists method checks if a file or directory exists
        public (bool, FileAttributes) Exists(string path)
        {
            // Check if path is a file or directory with FileAttributes
            FileAttributes attr = File.GetAttributes(path);
            switch (attr)
            {
                case FileAttributes.Directory:
                    return (true, attr);
                case FileAttributes.Archive:
                    return (true, attr);
                case FileAttributes.Device:
                    return (false, attr);
                case FileAttributes.Normal:
                    return (false, attr);
                default:
                    return (false, attr);
            }
            
        }
        
        public bool ExistsFile (string path)
        {
            return File.Exists(path);
        }
        public string DeleteDirectory(string path)
        {
            Directory.Delete(path, true);
            return "Directory deleted successfully";
        }
        public bool ExistsDirectory(string path)
        {
            return Directory.Exists(path);
        }
    }
}