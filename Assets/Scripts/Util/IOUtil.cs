using System.IO;
using UnityEngine;

namespace Util
{
    public class IOUtil : MonoBehaviour
    {
        public static string GetNameDirectory(string path)
        {
            // split when '/' && '\'is found
            string[] directories = path.Split('/', '\\');
            return directories[^1];
        }
        public static int TimesRepeatDir(string root, string baseName)
        {
            int count = 0;
            foreach (var dir in Directory.GetDirectories(root))
            {
                var nameDir = GetNameDirectory(dir);
                if (baseName == nameDir.Split("(")[0])
                {
                    count ++;
                }
            }

            return count;
        }
        public static bool ExistsDirectory(string path)
        {
            return Directory.Exists(path);
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string GetUniqueDirectoryName(string basePath, string baseName)
        {
            string uniqueName = baseName;
            int counter = 1;

            while (Directory.Exists(Path.Combine(basePath, uniqueName)))
            {
                uniqueName = $"{baseName}({counter})";
                counter++;
            }

            return uniqueName;
        }
    }
}