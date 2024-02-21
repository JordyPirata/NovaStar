using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetOverallVolume(float volume)
    {
        audioMixer.FindMatchingGroups("Master")[0].audioMixer.SetFloat("volume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.FindMatchingGroups("Music")[0].audioMixer.SetFloat("volume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.FindMatchingGroups("SFX")[0].audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
