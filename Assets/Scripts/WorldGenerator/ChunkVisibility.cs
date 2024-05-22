using Unity.Mathematics;
using System.Collections.Generic;
using Generator;
using UnityEngine;
using Map = Generator.ChunkGrid<Generator.ChunkObject>;
using Unity.Collections;
using Unity.Burst;
[BurstCompile]
public struct ChunkVisibility
{
    private static readonly List<ChunkObject> chunksVisible = new();
    private static readonly int chunkVisibleInViewDst = Mathf.RoundToInt(ChunkManager.maxViewDst / ChunkManager.width);
    public static void UpdateVisibleChunks(float2 viewerCoordinate)
    {

        foreach (var chunks in chunksVisible)
        {
            chunks.SetVisible(false);
        }
        chunksVisible.Clear();

        for (int yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
        {
            for (int xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
            {

                float2 viewedChunkCoord = new(viewerCoordinate.x + xOffset, viewerCoordinate.y + yOffset);
                if (Map.ContainsKey(viewedChunkCoord))
                {
                    Map.Instance[viewedChunkCoord].UpdateStatus();
                    if (Map.Instance[viewedChunkCoord].IsVisible())
                    {
                        chunksVisible.Add(Map.Instance[viewedChunkCoord]);
                    }
                }
                else
                {
                    Map.Add(viewedChunkCoord, ChunkGenerator.GenerateChunk(viewedChunkCoord));
                }

            }
        }
    }
}