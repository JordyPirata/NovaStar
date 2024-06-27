using Models;
using Services;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsMenu : MonoBehaviour
    {
        public void Start()
        {
            LoadSettings();
        }
        private ISettingsService SettingsService => ServiceLocator.GetService<ISettingsService>();
        public Button onButton, offButton, englishButton, spanishButton;
        public Slider overallVolumeSlider, musicVolumeSlider, sfxVolumeSlider, sensitibilitySlider;
        public void SetOverallVolume(float volume)
        {
            SettingsService.SetVolume(volume, ISettingsService.MasterGroup, "Master");
        }
        public void SetMusicVolume(float volume)
        {
            SettingsService.SetVolume(volume, ISettingsService.MusicGroup, "Music");
        }
        public void SetSFXVolume(float volume)
        {
            SettingsService.SetVolume(volume, ISettingsService.SFXGroup, "SFX");
        }
        public void SetSensitibility(float sensitibility)
        {
            SettingsService.SetSensitibility(sensitibility);
        }
        public void SetFullscreen(bool isFullscreen)
        {
            SettingsService.SetFullscreen(isFullscreen);
            SetButtons(isFullscreen, onButton, offButton);
        }
        public void SetLanguage(int language)
        {
            SettingsService.SetLanguage(language);
            // 0 is english, 1 is spanish
            SetButtons(language, englishButton, spanishButton);
        }
        // Load settings from file
        public async void LoadSettings()
        {
            Settings settings = await SettingsService.LoadSettings();
            overallVolumeSlider.value = settings.overallVolume;
            musicVolumeSlider.value = settings.musicVolume;
            sfxVolumeSlider.value = settings.sfxVolume;
            sensitibilitySlider.value = settings.mouseSensitivity;
            SetButtons(settings.fullscreen, onButton, offButton);
            SetButtons(settings.language, englishButton, spanishButton);
        }
        private void SetButtons(int language, Button englishButton, Button spanishButton)
        {
            englishButton.interactable = language == 1;
            spanishButton.interactable = language == 0;
        }
        // Save settings to file
        public void SaveSettings()
        {
            SettingsService.SaveSettings();
        }
        private void SetButtons(bool interactable, Button button1, Button button2)
        {
            button1.interactable = !interactable;
            button2.interactable = interactable;
        }
    }
}