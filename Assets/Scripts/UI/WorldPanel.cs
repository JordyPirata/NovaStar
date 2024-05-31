using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Repository;
using UnityEngine.UI;

public class WorldPanel : MonoBehaviour
{
    string message;
    public World game;
    // Access to the buttons of the world panel
    public void CreateWorld()
    {
        // Create a new world
        game = new World
        {
            // Set the game name
            GameName = "New Game",
            // Set the seed
            seed = Random.Range(0, int.MaxValue)
        };
        // Save the game
        SaveGame();
    }
    public async void SaveGame()
    {
        // Set the game directory
        game.GameDirectory = Path.Combine(Application.persistentDataPath, game.GameName);
        // Set the game path
        game.GamePath = Path.Combine(game.GameDirectory, string.Concat(game.GameName, ".bin"));
        // Create a folder with the game name
        Directory.CreateDirectory(game.GameDirectory);
        // Save the game
        message = await JsonRepository.Instance.Create(game, game.GamePath);
        
    }
    public void DeleteGame()
    {
        // Delete the game
        Directory.Delete(game.GameDirectory, true);
    }
}
