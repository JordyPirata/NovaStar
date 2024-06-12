using Unity.Mathematics;
using UnityEngine;
using Map = WorldGenerator.ChunkGrid<WorldGenerator.ChunkObject>;
using Unity.Burst;
using System.Threading.Tasks;
using WorldGenerator;

namespace Services
{
[BurstCompile]
public struct ChunkVisibility
{
    private static readonly int chunkVisibleInViewDst = Mathf.RoundToInt(ChunkConfig.maxViewDst / ChunkConfig.width);
    public static async Task UpdateVisibleChunks(float2 viewerCoordinate)
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
    public async Task UpdateChunks()
    {
        while (true)
        {   // get the viewer position and coordinate
            viewerPosition = new float2(viewer.position.x, viewer.position.z);
            viewerCoordinate = new float2(Mathf.RoundToInt(viewerPosition.x / width), Mathf.RoundToInt(viewerPosition.y / depth));
            // update the visible chunks
            await ChunkVisibility.UpdateVisibleChunks(viewerCoordinate);
            // delay the update of the chunks by system time
            await Task.Delay(2000);
            await Task.Yield();
        }
    }
}
}