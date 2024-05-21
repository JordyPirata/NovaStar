using UnityEngine;
using System.IO;


public class Game 
{
    public static string GameName; 
    public string GamePath = Path.Combine(Application.persistentDataPath, GameName);

}