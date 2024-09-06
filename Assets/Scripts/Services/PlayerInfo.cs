using UnityEngine;
using Unity.Mathematics;
using Unity.Burst;
using System.Collections;
using Config;
using Services.Interfaces;
using Services.Player;

namespace Services
{
/// <summary>
/// MonoState Service that holds the actual Player data
/// </summary>
public class PlayerInfo : MonoBehaviour, IPlayerInfo
{
    private Transform player;
    private static float2 viewerCoordinate;
    private static float3 viewerPosition;

    public void Init()
    {
        var PlayerObj = GameObject.Find("Player");
        if (PlayerObj != null) // Verifica si el objeto Player fue encontrado
        {
            player = PlayerObj.transform;
        }
    }
    public void StartService()
    {
        StartCoroutine(SetPlayerPosition());
    }
    public void StopService()
    {
        StopCoroutine(SetPlayerPosition());
    }

    /*public PlayerMediator GetPlayerMediator()
    {
        return playerMediator;
    }*/

    private IEnumerator SetPlayerPosition()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            viewerPosition = player.position;
            viewerCoordinate = new float2(Mathf.RoundToInt(viewerPosition.x / ChunkConfig.width), Mathf.RoundToInt(viewerPosition.z / ChunkConfig.depth));
        }
    }
    public float3 GetPlayerPosition()
    {
        if (player == null) // Verifica si player es null antes de acceder a su posición
        {
            return new float3(0, 0, 0); // Retorna una posición predeterminada o maneja la situación como prefieras
        }
        return viewerPosition;
    }
    public float2 GetPlayerCoordinate()
    {
        if (player == null) // Verifica si viewerPosition es null
        {
            return new float2(0, 0); // Retorna una coordenada predeterminada o maneja la situación como prefieras
        }
        return viewerCoordinate;
    }
}
}