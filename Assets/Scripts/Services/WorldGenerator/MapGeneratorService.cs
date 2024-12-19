using Unity.Mathematics;
using UnityEngine;
using System.Threading.Tasks;
using Config;
using Services.Interfaces;
using Services.Player;
using Services.WorldGenerator;
using UI;
using UnityEditor.SearchService;
using Unity.VisualScripting;
using System;
using System.Collections.Concurrent;
using Services.Installer;

namespace Services
{
/// <summary>
/// This class is responsible for showing the chunks that are visible to the player
/// </summary>

public class MapGeneratorService : MonoBehaviour, IMapGenerator, IService
{
    private Action Generate;
    private bool isRunning;
    bool firstLoop;
    private static IPlayerInfo PlayerInfo => ServiceLocator.GetService<IPlayerInfo>();
    private static IMap<ChunkObject> Map => ServiceLocator.GetService<IMap<ChunkObject>>();
    public void StartService()
    {
        firstLoop = true;
        isRunning = true;
    }
    private void OnDestroy()
    {
        StopService();
    }
    public void StopService()
    {
        isRunning = false;
        var map = Map as IDisposable;
        map.Dispose();
    }
    private static readonly int chunkVisibleInViewDst = Mathf.RoundToInt(ChunkConfig.maxViewDst / ChunkConfig.width);

    private void MapLoop(float2 viewerCoordinate)
    {
        for (var yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
        {
            for (var xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
            {

                float2 viewedChunkCoord = new(viewerCoordinate.x + xOffset, viewerCoordinate.y + yOffset);
                GenerateChunk(viewedChunkCoord);

            }
        }
        if (firstLoop) 
        {
            Debug.Log("Map Loaded");
            EventManager.OnMapLoaded.Invoke();
            firstLoop = false;
        }
        
    }
    private void GenerateChunk(float2 viewedChunkCoord)
    {
        
        if (Map.ContainsKey(viewedChunkCoord))
        {

            // if the chunk is already in the map, update the status of the chunk
            Map[viewedChunkCoord].UpdateStatus();
        }
        else
        {
            if (isRunning == false) return;
            var chunkBuilder = new ChunkBuilder(viewedChunkCoord);
            chunkBuilder.GenerateChunkData();
            chunkBuilder.SetGameObject();
            chunkBuilder.SetTerrain();
            chunkBuilder.CalculateBiomes();
            
            Map.Add(viewedChunkCoord, chunkBuilder.GetChunkObject());
        }
        
    }

    private async Task GenerateMap()
    {
        while (isRunning)
        {
            try
            {
                // delay the update of the chunks by system time
                await Task.Delay(3000);
                // get the player coordinate
                MapLoop(PlayerInfo.PlayerCoordinate());
                // delay the update of the chunks by system time
            }
            catch
            {
                Debug.Log("Chunk exit from map");
            }
            
        }
    }
}
}