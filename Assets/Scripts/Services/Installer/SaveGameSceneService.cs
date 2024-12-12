using System;
using System.IO;
using System.Threading.Tasks;
using Models;
using Services.Interfaces;
using Services.Repository;
using UnityEngine;

namespace Services.Installer
{
    public class SaveGameSceneService : MonoBehaviour, ISaveGameSceneService
    {
        private string _inventoryFile;
        private IRepository GameRepository => ServiceLocator.GetService<IRepository>();

        public void Awake()
        {
            
            _inventoryFile = Path.Combine(ServiceLocator.GetService<IWorldData>().GetDirectory(), "inventory.json");
            Debug.Log(Application.persistentDataPath);
        }

        private void Start()
        {
            LoadSettings();
        }

        public async void SaveState()
        {
            var message = await GameRepository.Create(
                ServiceLocator.GetService<IInventoryService>().GetModelState(),
                _inventoryFile);
            Debug.Log(message);
        }
        
        public async void LoadSettings()
        {
            if (!GameRepository.ExistsFile(_inventoryFile)) return;
            var (message, inventoryModel) = await GameRepository.Read<InventoryModel>(_inventoryFile);
            Debug.Log(message);
            ServiceLocator.GetService<IInventoryService>().LoadInventory(inventoryModel);
        }
    }
}