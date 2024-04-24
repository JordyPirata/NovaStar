using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;


public class Game : MonoBehaviour
{
    public static string GameName;
    private readonly string GamePath = Path.Combine(Application.persistentDataPath, GameName);

    public void Create()
    {
        // Create the game folder
        Directory.CreateDirectory(GamePath);
    }

    

    
}