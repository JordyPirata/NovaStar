using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class Serializer
{
    public void Serialize<T>(T data, string path)
    {
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(path, json);
    }

    public T Deserialize<T>(string path)
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(json);
    }
}
