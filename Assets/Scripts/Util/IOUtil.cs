using UnityEngine;
using System.IO;
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
}