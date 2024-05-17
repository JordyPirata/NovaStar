using Unity.Mathematics;
using System.Collections.Generic;
using Generator;
using UnityEngine;
using System.Collections;

public class ChunkVisibility
{
    private static ChunkVisibility instance;
    public static ChunkVisibility Instance
    {
        get
        {
            instance ??= new ChunkVisibility();
            return instance;
        }
    }
    private static readonly List<ChunkObject> chunksVisible = new();
    private readonly int chunkVisibleInViewDst = Mathf.RoundToInt(ChunkManager.maxViewDst / ChunkManager.width);
    public void UpdateVisibleChunks(ChunkGrid<ChunkObject> Chunks, float2 viewerCoordinate)
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
                if (Chunks.grid.ContainsKey(viewedChunkCoord))
                {
                    Chunks.grid[viewedChunkCoord].UpdateStatus();
                    if (Chunks.grid[viewedChunkCoord].IsVisible())
                    {
                        chunksVisible.Add(Chunks.grid[viewedChunkCoord]);
                    }
                }
                else
                {
                    Chunks.grid.Add(viewedChunkCoord, ChunkGenerator.GenerateChunk(viewedChunkCoord));
                }

            }
        }
    }
}