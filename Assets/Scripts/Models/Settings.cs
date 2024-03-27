using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{
    // Constructor for the settings class
    public Settings()
    {
        overallVolume = -50f;
        musicVolume = -50f;
        sfxVolume = -50f;
        fullscreen = true;
        language = 0;
        mouseSensitivity = 0.5f;
        return;
    }
    // Settings class to store the settings of the game
    public float overallVolume { get; set; }
    public float musicVolume { get; set; }
    public float sfxVolume { get; set; }
    public bool fullscreen { get; set; }
    public int language { get; set; }
    public float mouseSensitivity { get; set; }

}

