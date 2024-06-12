using Unity.Mathematics;
using UnityEngine;
using Map = WorldGenerator.ChunkGrid<WorldGenerator.ChunkObject>;
using Unity.Burst;
using System.Threading.Tasks;
using WorldGenerator;

namespace Services
{
/// <summary>
/// This class is responsible for showing the chunks that are visible to the player
/// </summary>
[BurstCompile]
public readonly struct MapGenerator: IMapGenerator
{
    private static float2 ViewerCoordinate
    {
        get
        {
            return ServiceLocator.GetService<IPlayerInfo>().GetPlayerCoordinate();
        }
    }
    public async void GenerateMap()
    {
        await UpdateChunks();
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

    private async readonly Task UpdateChunks()
    {
        while (true)
        {   
            // get the player coordinate
            
            await MapGenerator.UpdateVisibleChunks(ViewerCoordinate);
            // delay the update of the chunks by system time
            await Task.Delay(2000);
            await Task.Yield();
        }
    }
}
}