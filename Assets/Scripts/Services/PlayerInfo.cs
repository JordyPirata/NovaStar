using UnityEngine;
using Unity.Mathematics;
using static WorldGenerator.ChunkConfig;
using System.Collections;

namespace Services
{

public class PlayerInfo : MonoBehaviour , IPlayerInfo
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float2 viewerCoordinate;
    private float2 viewerPosition;
    public void Start()
    {
        StartCoroutine(SetPlayerPosition());
    }
    IEnumerator SetPlayerPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            UpdatePlayer();
        }
    }
    public void UpdatePlayer()
    {
        viewerPosition = new float2(player.position.x, player.position.z);
        viewerCoordinate = new float2(Mathf.RoundToInt(viewerPosition.x / width), Mathf.RoundToInt(viewerPosition.y / depth));
    }
    public float2 GetPlayerPosition()
    {
        return viewerPosition;
    }

    public float2 GetPlayerCoordinate()
    {
        return viewerCoordinate;
    }
    }

}