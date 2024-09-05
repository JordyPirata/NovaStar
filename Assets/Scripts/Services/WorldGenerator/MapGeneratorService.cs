using Unity.Mathematics;
using UnityEngine;
using Map = Services.WorldGenerator.ChunkGrid<Services.WorldGenerator.ChunkObject>;
using System.Threading.Tasks;
using Config;
using Services.Interfaces;
using Services.PlayerPath;
using Services.WorldGenerator;
using UI;

namespace Services
{
/// <summary>
/// This class is responsible for showing the chunks that are visible to the player
/// </summary>

public class MapGeneratorService : IMapGenerator
{
    private bool isRunning = false;
    private static IPlayerInfo PlayerInfo => ServiceLocator.GetService<IPlayerInfo>();
    public async void StartService()
    {
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

    private static async Task UpdateVisibleChunks(float2 viewerCoordinate)
    {

        for (var yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
        {
            for (var xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
            {

                float2 viewedChunkCoord = new(viewerCoordinate.x + xOffset, viewerCoordinate.y + yOffset);
                // if the chunk is in the map
                if (Map.ContainsKey(viewedChunkCoord))
                {
                    // update the status of the chunk
                    Map.Instance[viewedChunkCoord].UpdateStatus();
                }
                else
                {
                    Map.Add(viewedChunkCoord, await ChunkGenerator.GenerateChunk(viewedChunkCoord));
                }

            }
        }
    }

    private async Task GenerateMap()
    {
        bool firstLoop = true;
        while (isRunning)
        {
            // get the player coordinate
            await UpdateVisibleChunks(PlayerInfo.GetPlayerCoordinate());
            // delay the update of the chunks by system time
            await Task.Delay(2000);
            await Task.Yield();         
            if (firstLoop)
            {
                ServiceLocator.GetService<IFadeController>().FadeOut();
                ServiceLocator.GetService<IPlayerMediator>().MapLoaded();
                firstLoop = false;
            }
        }
    }
}
}