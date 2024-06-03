using UnityEngine;
using System.IO;
using System;

[Serializable]
public class World
{
    public World()
    {
        GameName = "NewGame";
    }
    public string GameName;
    public string GamePath;
    public string GameDirectory;
    public int seed;
}