using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public interface ISettingsService
{
    public const string Master = "Master";
    public const string Music = "Music";
    public const string SFX = "SFX";
    public IEnumerator SetLocale(int localeID);
    /// <summary>
    /// Use Constants from ISettingsService to set the volume of the audio mixer groups
    /// </summary>
    public void SetVolume(float volume, string groupingID, string floatName);
    // public void SetQuality(int qualityIndex);
    public void SetFullscreen(bool isFullscreen);
    // public void SetResolution(int resolutionIndex);
    public void SetLanguage(int language);
    public void SetSensitibility(float sensitibility);
    public void SaveSettings();
    public void LoadSettings();
}