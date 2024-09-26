using Unity.Mathematics;
using UnityEngine;
using System.Threading.Tasks;
using Config;
using Services.Interfaces;
using Services.Player;
using Services.WorldGenerator;
using UI;
using UnityEditor.SearchService;

namespace Services
{
/// <summary>
/// This class is responsible for showing the chunks that are visible to the player
/// </summary>

public class MapGeneratorService : IMapGenerator
{
    private bool isRunning;
    bool firstLoop;
    private static IPlayerInfo PlayerInfo => ServiceLocator.GetService<IPlayerInfo>();
    private static IMap<ChunkObject> Map => ServiceLocator.GetService<IMap<ChunkObject>>();
    public async void StartService()
    {
        firstLoop = true;
        isRunning = true;
        await GenerateMap();
    }
    public void StopService()
    {
        isRunning = false;
        foreach (var chunk in Map.AllChunks())
        {
            chunk.Release();
        }
        
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
    private async void GenerateChunk(float2 viewedChunkCoord)
    {
        if (Map.ContainsKey(viewedChunkCoord))
        {
            // update the status of the chunk
            Map[viewedChunkCoord].UpdateStatus();
        }
        else
        {
            Map.Add(viewedChunkCoord, await ChunkGenerator.GenerateChunk(viewedChunkCoord));
        }
    }

    private async Task GenerateMap()
    {
        while (isRunning)
        {
            // delay the update of the chunks by system time
            await Task.Delay(3000);
            // get the player coordinate
            MapLoop(PlayerInfo.PlayerCoordinate());
            // delay the update of the chunks by system time
            
        }
    }
}
}