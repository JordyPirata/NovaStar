using UnityEngine;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        public void Start()
        {
            ServiceLocator.GetService<ISettingsService>().LoadSettings();
        }
        public void PlayGame()
        {
            // Load the game scene
            Debug.Log("Play Game");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        
        }
        public void QuitGame()
        {
            // Quit the game
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
}