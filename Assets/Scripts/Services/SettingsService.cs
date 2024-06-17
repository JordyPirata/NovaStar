using UnityEngine.Audio;
using System.IO;
using UnityEngine;
using Console = UnityEngine.Debug;
using UnityEngine.Localization.Settings;
using System.Collections;
using Unity.VisualScripting;
namespace Services
{
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
    public async void LoadSettings()
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
    }

    public async void SaveSettings()
    {
        message = await GameRepository.Create(settings, settingsFile);
        Console.Log(message);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        settings.fullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
    
    public void SetLanguage(int language)
    {
        settings.language = language;
        StartCoroutine(SetLocale(language));
    }
    public IEnumerator SetLocale(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }

    public void SetSensitibility(float sensitibility)
    {
        settings.mouseSensitivity = sensitibility;
    }

    public void SetVolume(float volume, string groupingID, string floatName)
    {
        Mixer.FindMatchingGroups(groupingID)[0].audioMixer.SetFloat(floatName, volume);
        switch (groupingID)
        {
            case ISettingsService.Master:
                settings.overallVolume = volume;
                break;
            case ISettingsService.Music:
                settings.musicVolume = volume;
                break;
            case ISettingsService.SFX:
                settings.sfxVolume = volume;
                break;
        }
    }
}
}