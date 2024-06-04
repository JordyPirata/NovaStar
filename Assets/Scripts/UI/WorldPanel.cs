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
    public void Awake()
    {
        CreateWorld();
    }
    // Variables
    string message;
    public World game;
    //Access to TextMeshPro component
    public TMP_InputField TMPro;

    // Access to the buttons of the world panel
    public void CreateWorld()
    {
        game = new World
        {
            // Set the game name
            GameName = TMPro.text,
            // Set the seed
            seed = 12345,
        };
        game.GameDirectory = Path.Combine(Application.persistentDataPath, game.GameName);
        // Save the game
        SaveGame();
    }
    public async void UpdateWorld()
    {
        // Update the game
        message = await GameRepository.Instance.Create(game, game.GamePath);
        Console.Log(message);
    }
    public async void SaveGame()
    {
        // Check if the game already exists
        if(GameRepository.ExistsDirectory(game.GameDirectory))
        {
            // Set the game name add (n) to the name an increment it
            game.GameName = string.Concat(game.GameName, " (", Directory.GetDirectories(Application.persistentDataPath).Length, ")");
        }
        // Set the game name
        game.GameName = TMPro.text;
        // Set the game directory
        game.GameDirectory = Path.Combine(Application.persistentDataPath, game.GameName);
        // Set the game path
        game.GamePath = Path.Combine(game.GameDirectory, string.Concat(game.GameName, ".bin"));
        // Create a folder with the game name
        Directory.CreateDirectory(game.GameDirectory);
        // Save the game
        message = await GameRepository.Instance.Create(game, game.GamePath);
        Console.Log(message);
        
    }
    
    public async void LoadGame()
    {
        // Load the game
        (message, game) = await GameRepository.Instance.Read<World>(game.GamePath);
        Console.Log(message);
    }

    public void DeleteGame()
    {
        // Delete the game
        Directory.Delete(game.GameDirectory, true);
        Destroy(gameObject);
    }
        
}
