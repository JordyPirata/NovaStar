using Unity.Mathematics;
using System.Collections.Generic;
using Generator;
using UnityEngine;
using System.Collections;
using Map = Generator.ChunkGrid<Generator.ChunkObject>;

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
    public void UpdateVisibleChunks(float2 viewerCoordinate)
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