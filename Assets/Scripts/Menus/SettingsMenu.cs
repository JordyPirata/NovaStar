using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using Console = UnityEngine.Debug;

public class SettingsMenu : MonoBehaviour
{
    public Button onButton, offButton, EnglishButton, SpanishButton;
    private Settings settings = new();
    public AudioMixer audioMixer;
    public void SetOverallVolume(float volume)
    {
        audioMixer.FindMatchingGroups("Master")[0].audioMixer.SetFloat("Master", volume);
        settings.overallVolume = volume;
        Console.Log("Volume: " + volume);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.FindMatchingGroups("Music")[0].audioMixer.SetFloat("Music", volume);
        settings.musicVolume = volume;
        Console.Log("Volume: " + volume);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.FindMatchingGroups("SFX")[0].audioMixer.SetFloat("SFX", volume);
        settings.sfxVolume = volume;
        Console.Log("Volume: " + volume);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        settings.fullscreen = isFullscreen;
        if (isFullscreen)
        {
            onButton.interactable = false;
            offButton.interactable = true;
        }
        else
        {
            offButton.interactable = false;
            onButton.interactable = true;
        }
    }
    public void SetEnglish()
    {
        settings.language = 0;
        StartCoroutine(SetLocale(0));
        EnglishButton.interactable = false;
        SpanishButton.interactable = true;
    }
    public void SetSpanish()
    {
        settings.language = 1;
        StartCoroutine(SetLocale(1));
        SpanishButton.interactable = false;
        EnglishButton.interactable = true;
    }
    IEnumerator SetLocale(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }
    public void SaveSettings()
    {
        CRUD crud = new();
        crud.Create(settings, Application.persistentDataPath + "/settings.json");
        Console.Log(Application.persistentDataPath + "/settings.json");
    }
    /*
    public void LoadSettings()
    {
        CRUD crud = new();
        settings = crud.Read<Settings>(Application.persistentDataPath + "/settings.json");
        SetOverallVolume(settings.overallVolume);
        SetMusicVolume(settings.musicVolume);
        SetSFXVolume(settings.sfxVolume);
        SetFullscreen(settings.fullscreen);
        if (settings.language == 0)
        {
            EnglishButton.interactable = false;
            SpanishButton.interactable = true;
        }
        else
        {
            SpanishButton.interactable = false;
            EnglishButton.interactable = true;
        }
    }*/
}