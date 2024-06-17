using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class SettingsMenu : MonoBehaviour
    {
        private ISettingsService SettingsService => ServiceLocator.GetService<ISettingsService>();
    
        public Button onButton, offButton, englishButton, spanishButton;
        public Slider overallVolumeSlider, musicVolumeSlider, sfxVolumeSlider, sensitibilitySlider;
        public void SetOverallVolume(float volume)
        {
            SettingsService.SetVolume(volume, ISettingsService.Master, "Master");
        }
        public void SetMusicVolume(float volume)
        {
            SettingsService.SetVolume(volume, ISettingsService.Music, "Music");
        }
        public void SetSFXVolume(float volume)
        {
            SettingsService.SetVolume(volume, ISettingsService.SFX, "SFX");
        }
        public void SetSensitibility(float sensitibility)
        {
            SettingsService.SetSensitibility(sensitibility);
        }
        public void SetFullscreen(bool isFullscreen)
        {
            SettingsService.SetFullscreen(isFullscreen);
            if (isFullscreen)
            {
                onButton.interactable = false;
                offButton.interactable = true;
                return;
            }
            offButton.interactable = false;
            onButton.interactable = true;
            
        }
        public void SetLanguage(int language)
        {
            SettingsService.SetLanguage(language);
            if (language == 0)
            {
                englishButton.interactable = false;
                spanishButton.interactable = true;
                return;
            }
            spanishButton.interactable = false;
            englishButton.interactable = true;
            
        }

        // Load settings from file
        public void LoadSettings()
        {
            SettingsService.LoadSettings();
        }
        // Save settings to file
        public void SaveSettings()
        {
            SettingsService.SaveSettings();
        }
    }
}