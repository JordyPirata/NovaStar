using UnityEngine;
using System.IO;
using Services;
using TMPro;
using Console = UnityEngine.Debug;
using UnityEngine.UI;
using UnityEngine.Localization;

public class WorldPanel : MonoBehaviour
{
    private IRepository GameRepository;
    void Awake()
    {
        GameRepository = ServiceLocator.GetService<IRepository>();
    }
    // Variables
    string message;
    public World game;
    //Access to TextMeshPro component
    public TMP_InputField TMPro;
    public Button editButton;
    public Button playButton;
    public LocalizedString localizedString;

    // Access to the buttons of the world panel
    public void CreateWorld(World _game)
    {
        if (_game == null)
        {
            game = new World
            {
                // Set the game name
                GameName = TMPro.text,
                // Set the seed
                seed = Random.Range(0, int.MaxValue)
            };
        }
        game = _game;
        TMPro.text = game.GameName;
        
        // Create a folder with the game name
        game.GameDirectory = Path.Combine(Application.persistentDataPath, game.GameName);
        // Set the game path
        game.GamePath = Path.Combine(game.GameDirectory, string.Concat(game.GameName, ".bin"));
        // Save the game
    }
    public async void UpdateWorld()
    {
        if (!GameRepository.ExistsDirectory(game.GameDirectory))
        {
            Console.Log("Game not found");
            return;
        }
        _ = GameRepository.Delete(game.GamePath);
        // Rename Directory
        Directory.Move(game.GameDirectory, Path.Combine(Application.persistentDataPath, TMPro.text));
        // Set the game name
        game.GameName = TMPro.text;
        UpdateDir();
        message = await GameRepository.Create(game, game.GamePath);
    }

    public async void SaveWorld()
    {
        // Check if the game already exists
        if(GameRepository.ExistsDirectory(game.GameDirectory))
        {
            int o = IOUtil.TimesRepeatDir(Application.persistentDataPath, game.GameName);
            // Set the game name add (n) to the name an increment it
            game.GameName = string.Concat(game.GameName, "(", o, ")");
            // Set the text of the TextMeshPro component
            TMPro.text = game.GameName;
            UpdateDir();
        }
        // Create a folder with the game name
        Directory.CreateDirectory(game.GameDirectory);
        // Save the game
        message = await GameRepository.Create(game, game.GamePath);
        Console.Log(message);
        
    }
    private void UpdateDir()
    {
        // Set the game directory path
        game.GameDirectory = Path.Combine(Application.persistentDataPath, game.GameName);
        // Set the game path
        game.GamePath = Path.Combine(game.GameDirectory, string.Concat(game.GameName, ".bin"));
    }

    public void DeleteGame()
    {
        // Delete the game
        Directory.Delete(game.GameDirectory, true);
        Destroy(gameObject);
    }
    public async void LoadWorld(string directoryPath)
    {
        
        string GameName = IOUtil.GetNameDirectory(directoryPath);
        string GamePath = Path.Combine(directoryPath, string.Concat(GameName, ".bin"));
        // Load the game
        (message, game) = await GameRepository.Read<World>(GamePath);
        CreateWorld(game);
        Console.Log(message);
    }
        
}
