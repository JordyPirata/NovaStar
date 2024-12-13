using System;
using System.IO;
using System.Threading.Tasks;
using Models;
using Services.Interfaces;
using UnityEngine;

namespace Services.Installer
{
    public class SaveGameSceneService : MonoBehaviour, ISaveGameSceneService
    {
        private string _inventoryFile, _playerStatsFile;
        private IRepository GameRepository => ServiceLocator.GetService<IRepository>();

        public void Awake()
        {
            _inventoryFile = Path.Combine(ServiceLocator.GetService<IWorldData>().GetDirectory(), "inventory.json");
            _playerStatsFile = Path.Combine(ServiceLocator.GetService<IWorldData>().GetDirectory(), "playerStats.json");
        }

        private void Start()
        {
            LoadInventory();
        }

        public async void SaveState()
        {
            await GameRepository.CreateAsync(
                ServiceLocator.GetService<IInventoryService>().GetModelState(),
                _inventoryFile);

            var playerStatsModel = new PlayerStatsModel()
            {
                playerPosition = ServiceLocator.GetService<IPlayerInfo>().PlayerPosition(),
                playerLife = ServiceLocator.GetService<IPlayerMediator>().GetLife(),
                playerThirsty = ServiceLocator.GetService<IPlayerMediator>().GetThirsty(),
                playerHunger = ServiceLocator.GetService<IPlayerMediator>().GetHunger(),
            };

            await GameRepository.CreateAsync(playerStatsModel, _inventoryFile);
        }

        public async void LoadInventory()
        {
            if (!GameRepository.ExistsFile(_inventoryFile)) return;
            var (message, inventoryModel) = await GameRepository.ReadAsync<InventoryModel>(_inventoryFile);
            ServiceLocator.GetService<IInventoryService>().LoadInventory(inventoryModel);
            
            if (!GameRepository.ExistsFile(_playerStatsFile)) return;
            var (playerStatsMessage, playerStatsModel) =
                await GameRepository.ReadAsync<PlayerStatsModel>(_playerStatsFile);
            
            
        }
    }
}