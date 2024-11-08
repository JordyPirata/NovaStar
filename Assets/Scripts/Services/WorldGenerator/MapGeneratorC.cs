using Unity.Mathematics;
using UnityEngine;
using System.Threading.Tasks;
using Config;
using Services.Interfaces;
using Services.Player;
using Services.WorldGenerator;
using Models;
using System;


namespace Services
{
/// <summary>
/// This class is responsible for showing the chunks that are visible to the player
/// </summary>

public class MapGeneratorC : MonoBehaviour, IMapGenerator
{
    private IMap<ChunkObject> Map;
    void Awake()
    {
        Map = ServiceLocator.GetService<IMap<ChunkObject>>();
    }
    private static IPlayerInfo PlayerInfo => ServiceLocator.GetService<IPlayerInfo>();
    private bool run = false;
    public void StartService()
    {
        run = true;
    }
    public void StopService()
    {
        run = false;
    }
    private static readonly int chunkVisibleInViewDst = Mathf.RoundToInt(ChunkConfig.maxViewDst / ChunkConfig.width);

    private void UpdateVisibleChunks(float2 viewerCoordinate)
    {
        for (var yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
        {
            for (var xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
            {

                float2 viewedChunkCoord = new(viewerCoordinate.x + xOffset, viewerCoordinate.y + yOffset);
                GenerateChunk(viewedChunkCoord);

            }
        }
    }

    private async void GenerateChunk (float2 viewedChunkCoord)
    {
        if (Map.ContainsKey(viewedChunkCoord))
        {
            // update the status of the chunk
            Map[viewedChunkCoord].UpdateStatus();
        }
        else
        {
            var chunkBuilder = new ChunkBuilder(viewedChunkCoord);
            await chunkBuilder.GenerateChunkData();
            chunkBuilder.SetTerrain();
            Map.Add(viewedChunkCoord, chunkBuilder.GetChunkObject());
        }
    }
    private void Update()
    {
        if (run)
        {
            UpdateVisibleChunks(PlayerInfo.PlayerCoordinate());
        }
    }
}
}