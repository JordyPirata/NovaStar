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
    // TODO: FIX 
    public class WorldPanel : MonoBehaviour
    {
        private IWorldCRUD WorldCRUD;
        private IWorldData WorldData;
        void Awake()
        {
            WorldCRUD = ServiceLocator.GetService<IWorldCRUD>();
            WorldData = ServiceLocator.GetService<IWorldData>();
        }

        // Variables
        public World game;
        //Access to TextMeshPro component
        public TMP_InputField TMPro;
        public Button editButton;
        public Button playButton;
        public Button deleteButton;
        public LocalizedString localizedString;

        public void CreateWorld()
        {
            game = WorldCRUD.CreateWorld(TMPro.text);
            TMPro.text = game.Name;
        }
        public void UpdateWorld()
        {
            // Update the game
            WorldCRUD.UpdateWorld(game, TMPro.text);
            game.Name = TMPro.text;
        }

        public void SaveWorld()
        {
            // Save the game
            WorldCRUD.SaveWorld(game);
        }

        public void DeleteGame()
        {
            // Delete the game
            Directory.Delete(game.Directory, true);
            Destroy(gameObject);
        }
        public async void ReadWorld(string directoryPath)
        {
            game = await WorldCRUD.ReadWorld(directoryPath);
            if (game.IsGenerated)
            {
                editButton.interactable = false;
            }
            TMPro.text = game.Name;
        }
        public void PlayGame()
        {
            ServiceLocator.GetService<IFadeController>().FadeIn(()=>
            {
                WorldData.SetWorld(game);
                WorldData.SetIsGenerated(true);
                WorldData.SaveWorld();
                ServiceLocator.GetService<ISceneLoader>().LoadScene(ISceneLoader.Game); 
            });
        }
    }
}