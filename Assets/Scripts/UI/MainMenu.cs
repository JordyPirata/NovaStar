using UnityEngine;

namespace Menus
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