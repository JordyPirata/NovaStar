using UnityEngine;
using System.IO;
using Services;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization;

public class WorldPanel : MonoBehaviour
{
    private ICreateWorld GameGenerator;
    void Awake()
    {
        GameGenerator = ServiceLocator.GetService<ICreateWorld>();
    }
    // Variables
    public World game;
    //Access to TextMeshPro component
    public TMP_InputField TMPro;
    public Button editButton;
    public Button playButton;
    public LocalizedString localizedString;

    // Access to the buttons of the world panel
    /// <summary>
    /// Set Null for Create new World
    /// </summary>
    /// <param name="_game"></param>
    public void SetWorld(World _game)
    {
        game = _game;
    }
    public void CreateWorld()
    {
        game = ServiceLocator.GetService<ICreateWorld>().CreateWorld(TMPro.text);
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
        TMPro.text = game.Name;
    }

    public void DeleteGame()
    {
        // Delete the game
        Directory.Delete(game.Directory, true);
        Destroy(gameObject);
    }
    public async void LoadWorld(string directoryPath)
    {
        game = await GameGenerator.LoadWorld(directoryPath);
        TMPro.text = game.Name;
    }
    public void PlayGame()
    {
        // Load the game
        ServiceLocator.GetService<ISceneLoader>().LoadScene(ISceneLoader.Game);
    }
}
