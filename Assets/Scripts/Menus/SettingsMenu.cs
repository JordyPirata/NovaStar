using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using Console = UnityEngine.Debug;

public class SettingsMenu : MonoBehaviour
{
    public Button onButton, offButton, englishButton, spanishButton;
    public Slider overallVolumeSlider, musicVolumeSlider, sfxVolumeSlider, sensitibilitySlider;
    private Settings settings = new();
    private readonly CRUD crud = new();
    public AudioMixer audioMixer;

    private void Awake()
    {
        LoadSettings();
    }

    public void SetOverallVolume(float volume)
    {
        audioMixer.FindMatchingGroups("Master")[0].audioMixer.SetFloat("Master", volume);
        settings.overallVolume = volume;
        
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.FindMatchingGroups("Music")[0].audioMixer.SetFloat("Music", volume);
        settings.musicVolume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.FindMatchingGroups("SFX")[0].audioMixer.SetFloat("SFX", volume);
        settings.sfxVolume = volume;
    }
    public void SetSensitibility(float sensitibility)
    {
        settings.mouseSensitivity = sensitibility;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        if (isFullscreen)
        {
            settings.fullscreen = true;
            onButton.interactable = false;
            offButton.interactable = true;
        }
        else
        {
            settings.fullscreen = false;
            offButton.interactable = false;
            onButton.interactable = true;
        }
        Screen.fullScreen = isFullscreen;
    }
    public void SetLanguage(int language)
    {
        if (language == 0)
        {
            settings.language = 0;
            StartCoroutine(SetLocale(0));
            englishButton.interactable = false;
            spanishButton.interactable = true;
        }
        else
        {
            settings.language = 1;
            StartCoroutine(SetLocale(1));
            spanishButton.interactable = false;
            englishButton.interactable = true;
        }
    }
    IEnumerator SetLocale(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }
    public void SaveSettings()
    {
        crud.Create(settings, Application.persistentDataPath + "/settings.json");
    }

    public void LoadSettings()
    {
        settings = crud.Read<Settings>(Application.persistentDataPath + "/settings.json");

        SetLanguage(settings.language);
        SetFullscreen(settings.fullscreen);

        overallVolumeSlider.value = settings.overallVolume;
        musicVolumeSlider.value = settings.musicVolume;
        sfxVolumeSlider.value = settings.sfxVolume;

        sensitibilitySlider.value = settings.mouseSensitivity;
    }
}