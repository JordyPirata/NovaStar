using Services;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void Start()
        {
            ServiceLocator.GetService<ISettingsService>().LoadSettings();
        }
        public void QuitGame()
        {
            // Quit the game
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
}