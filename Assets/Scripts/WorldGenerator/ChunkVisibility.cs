using Unity.Mathematics;
using UnityEngine;
using Map = Generator.ChunkGrid<Generator.ChunkObject>;
using Unity.Burst;
using System.Threading.Tasks;
[BurstCompile]
public struct ChunkVisibility
{
    private static readonly int chunkVisibleInViewDst = Mathf.RoundToInt(ChunkManager.maxViewDst / ChunkManager.width);
    public static async Task UpdateVisibleChunks(float2 viewerCoordinate)
    {

        for (var yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
        {
            for (var xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
            {

                float2 viewedChunkCoord = new(viewerCoordinate.x + xOffset, viewerCoordinate.y + yOffset);
                if (Map.ContainsKey(viewedChunkCoord))
                {
                    Map.Instance[viewedChunkCoord].UpdateStatus();
                }
                else
                {
                    Map.Add(viewedChunkCoord, await ChunkGenerator.GenerateChunk(viewedChunkCoord));
                }

            }
        }
    }
}