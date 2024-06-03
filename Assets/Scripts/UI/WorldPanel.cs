using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Repository;
using UnityEngine.UI;

using TMPro;
using Console = UnityEngine.Debug;

public class WorldPanel : MonoBehaviour
{
    string message;
    public World game;
    //Access to TextMeshPro component
    public TextMeshProUGUI worldName;

    public void Awake()
    {
        CreateWorld();
    }
    // Access to the buttons of the world panel
    public void CreateWorld()
    {
        game = new World
        {
            // Set the game name
            GameName = worldName.text,
            // Set the seed
            seed = 12345,
        };
        game.GameDirectory = Path.Combine(Application.persistentDataPath, game.GameName);
        Console.Log(game.GameDirectory);
        // Check if the game already exists
        if(JsonRepository.ExistsDirectory(game.GameDirectory))
        {
            // Set the game name add (n) to the name an increment it
            game.GameName = string.Concat(game.GameName, " (", Directory.GetDirectories(Application.persistentDataPath).Length, ")");
        }
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
        // Destroy the game object
        Destroy(gameObject);
    }
}
