using UnityEngine.Audio;
using System.IO;
using UnityEngine;
using Console = UnityEngine.Debug;
using UnityEngine.Localization.Settings;
using System.Collections;
using Unity.VisualScripting;
using System.Threading.Tasks;
namespace Services
{
/// <summary>
/// CRUD operations for the settings
/// </summary>
public class SettingsService : MonoBehaviour, ISettingsService
{
    private AudioMixer Mixer => Resources.Load<AudioMixer>("MainMixer");
    private IRepository GameRepository => ServiceLocator.GetService<IRepository>();
    private static string message;
    private static string settingsFile;
    private static Settings settings = new();
    public void Awake()
    {
        settingsFile = Path.Combine(Application.persistentDataPath, "settings.bin");
    }
    public async Task<Settings> LoadSettings()
    {
        if (!GameRepository.ExistsFile(settingsFile))
        {
            settings = new Settings();
            SaveSettings();
        }
        else
        {
            (message, settings) = await GameRepository.Read<Settings>(settingsFile);
            Console.Log(message);
        }
        SetLanguage(settings.language);
        SetFullscreen(settings.fullscreen);
        SetVolume(settings.overallVolume, ISettingsService.MasterGroup, "Master");
        SetVolume(settings.musicVolume, ISettingsService.MusicGroup, "Music");
        SetVolume(settings.sfxVolume, ISettingsService.SFXGroup, "SFX");
        SetSensitibility(settings.mouseSensitivity);
        return settings;
    }

    public async void SaveSettings()
    {
        message = await GameRepository.Create(settings, settingsFile);
        Console.Log(message);
    }

    public bool SetFullscreen(bool isFullscreen)
    {
        settings.fullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
        return isFullscreen;
    }
    
    public int SetLanguage(int language)
    {
        settings.language = language;
        StartCoroutine(SetLocale(language));
        return language;
    }
    public IEnumerator SetLocale(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }

    public float SetSensitibility(float sensitibility)
    {
        settings.mouseSensitivity = sensitibility;
        return sensitibility;
    }

    public float SetVolume(float volume, string groupingID, string floatName)
    {
        Mixer.FindMatchingGroups(groupingID)[0].audioMixer.SetFloat(floatName, volume);
        switch (groupingID)
        {
            case ISettingsService.MasterGroup:
                settings.overallVolume = volume;
                break;
            case ISettingsService.MusicGroup:
                settings.musicVolume = volume;
                break;
            case ISettingsService.SFXGroup:
                settings.sfxVolume = volume;
                break;
        }
        return volume;
    }
}
}