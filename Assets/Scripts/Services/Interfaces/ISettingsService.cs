using System.Collections;
using System.Threading.Tasks;
using Models;

namespace Services.Interfaces
{
    public interface ISettingsService
    {
        public const string MasterGroup = "Master";
        public const string MusicGroup = "Music";
        public const string SFXGroup = "SFX";
        public IEnumerator SetLocale(int localeID);
        /// <summary>
        /// Use Constants from ISettingsService to set the volume of the audio mixer groups
        /// </summary>
        public float SetVolume(float volume, string groupingID, string floatName);
        // public void SetQuality(int qualityIndex);
        public bool SetFullscreen(bool isFullscreen);
        // public void SetResolution(int resolutionIndex);
        public int SetLanguage(int language);
        public float SetSensitibility(float sensitibility);
        public void SaveSettings();
        public Task<Settings> LoadSettings();
        public float GetSensitibility();
    }
}