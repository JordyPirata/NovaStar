using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;

public class CRUD
{
    private Serializer serializer = new Serializer();
    public void Create<T>(T data, string path)
    {
        serializer.Serialize(data, path);
    }

    public T Read<T>(string path)
    {
        return serializer.Deserialize<T>(path);
    }

    public void Update<T>(T data, string path)
    {
        serializer.Serialize(data, path);
    }

    public void Delete(string path)
    {
        File.Delete(path);
    }
}