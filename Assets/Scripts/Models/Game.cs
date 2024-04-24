using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;


public class Game 
{
    public static string GameName;
    public string GamePath = Path.Combine(Application.persistentDataPath, GameName);

}