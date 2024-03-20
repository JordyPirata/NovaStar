using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        public void QuitGame()
        {
            // Quit the game
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
}