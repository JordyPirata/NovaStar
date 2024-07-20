using System;
using System.IO;
using Models;
using Services;
using Services.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;


namespace UI
{
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
        public Button deleteButton;
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
        public void EditWorld()
        {
            // Set the world
            ServiceLocator.GetService<IWorldData>().SetWorld(game);
        }
        public void PlayGame()
        {
            ServiceLocator.GetService<IWorldData>().SetWorld(game);
            ServiceLocator.GetService<ISceneLoader>().LoadScene(ISceneLoader.Game);
        
        }
    }
}
