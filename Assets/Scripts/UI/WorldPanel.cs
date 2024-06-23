using UnityEngine;
using System.IO;
using Services;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization;

public class WorldPanel : MonoBehaviour
{
    private ICreateGame GameGenerator;
    void Awake()
    {
        GameGenerator = ServiceLocator.GetService<ICreateGame>();
    }
    // Variables
    public World game;
    //Access to TextMeshPro component
    public TMP_InputField TMPro;
    public Button editButton;
    public Button playButton;
    public LocalizedString localizedString;

    // Access to the buttons of the world panel
    public void CreateWorld(World _game)
    {
        game = ServiceLocator.GetService<ICreateGame>().CreateWorld(TMPro.text) ?? _game;
    }
    public async void UpdateWorld()
    {
        // Update the game
        game = await GameGenerator.UpdateWorld(game, TMPro.text);
    }

    public async void SaveWorld()
    {
        // Save the game
        game = await GameGenerator.SaveWorld(game);
        TMPro.text = game.GameName;
    }

    public void DeleteGame()
    {
        // Delete the game
        Directory.Delete(game.GameDirectory, true);
        Destroy(gameObject);
    }
    public async void LoadWorld(string directoryPath)
    {
        game = await GameGenerator.LoadWorld(directoryPath);
        TMPro.text = game.GameName;
    }
    public void PlayGame()
    {
        // Load the game
        ServiceLocator.GetService<ISceneLoader>().LoadScene(ISceneLoader.Game);
    }
}
