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
    private void Awake()
    {
        if (Screen.fullScreen)
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

    public AudioMixer audioMixer;
    public void SetOverallVolume(float volume)
    {
        audioMixer.FindMatchingGroups("Master")[0].audioMixer.SetFloat("Master", volume);
        Console.Log("Volume: " + volume);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.FindMatchingGroups("Music")[0].audioMixer.SetFloat("Music", volume);
        Console.Log("Volume: " + volume);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.FindMatchingGroups("SFX")[0].audioMixer.SetFloat("SFX", volume);
        Console.Log("Volume: " + volume);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
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
        StartCoroutine(SetLocale(0));
        EnglishButton.interactable = false;
        SpanishButton.interactable = true;
    }
    public void SetSpanish()
    {
        StartCoroutine(SetLocale(1));
        SpanishButton.interactable = false;
        EnglishButton.interactable = true;
    }
    IEnumerator SetLocale(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }
}