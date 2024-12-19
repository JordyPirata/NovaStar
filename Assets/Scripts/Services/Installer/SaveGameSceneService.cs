using System;
using System.IO;
using System.Threading.Tasks;
using Models;
using Services.Interfaces;
using Services.Player;
using UnityEngine;

namespace Services.Installer
{
    public class SaveGameSceneService : MonoBehaviour, ISaveGameSceneService
    {
        private string _inventoryFile, _playerStatsFile, _teleportsFile;
        private IRepository GameRepository => ServiceLocator.GetService<IRepository>();

        private void OnEnable()
        {
            EventManager.OnMapLoaded += LoadState;
        }

        private void OnDisable()
        {
            EventManager.OnMapLoaded -= LoadState;
        }

        public void Awake()
        {
            _inventoryFile = Path.Combine(ServiceLocator.GetService<IWorldData>().GetDirectory(), "inventory.bin");
            _playerStatsFile = Path.Combine(ServiceLocator.GetService<IWorldData>().GetDirectory(), "playerStats.bin");
            _teleportsFile = Path.Combine(ServiceLocator.GetService<IWorldData>().GetDirectory(), "teleportsFile.bin");
        }

        public async void SaveState()
        {
            await GameRepository.CreateAsync(
                ServiceLocator.GetService<IInventoryService>().GetModelState(),
                _inventoryFile);

            await GameRepository.CreateAsync(ServiceLocator.GetService<IPlayerMediator>().GetPlayerStatsModel(), _playerStatsFile);
            
            await GameRepository.CreateAsync(ServiceLocator.GetService<ITeleportService>().GetTeleportsModel(), _teleportsFile);            
            
        }

        public async void LoadState()
        {
            if (!GameRepository.ExistsFile(_inventoryFile)) return;
            var (message, inventoryModel) = await GameRepository.ReadAsync<InventoryModel>(_inventoryFile);
            ServiceLocator.GetService<IInventoryService>().LoadInventory(inventoryModel);
            
            if (!GameRepository.ExistsFile(_playerStatsFile)) return;
            var (playerStatsMessage, playerStatsModel) =
                await GameRepository.ReadAsync<PlayerStatsModel>(_playerStatsFile);
            ServiceLocator.GetService<IPlayerMediator>().LoadPlayerStats(playerStatsModel);
            
            if (!GameRepository.ExistsFile(_teleportsFile)) return;
            var (teleportsMessage, teleportsModel) =
                await GameRepository.ReadAsync<TeleportsModel>(_teleportsFile);
            ServiceLocator.GetService<ITeleportService>().LoadTeleports(teleportsModel);
        }
    }
}