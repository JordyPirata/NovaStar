using UnityEngine;
using Unity.Mathematics;
using Unity.Burst;
using System.Collections;
using Config;
using Services.Interfaces;
using UnityEngine.SceneManagement;
using Services.WorldGenerator;

namespace Services
{
/// <summary>
/// MonoState Service that holds the actual Player data
/// </summary>
public class PlayerInfo : MonoBehaviour, IPlayerInfo
{
    private IMap<ChunkObject> Map;
    private Transform player;
    private static float2 viewerCoordinate;
    private static float3 viewerPosition;
    private bool isRunning;

    public void Awake()
    {
        EventManager.OnSceneGameLoaded += FindPlayer;
        Map = ServiceLocator.GetService<IMap<ChunkObject>>();
    }
    private void FindPlayer()
    {
        var PlayerObj = GameObject.Find("Player");
        if (PlayerObj != null) // Verifica si el objeto Player fue encontrado
        {
            player = PlayerObj.transform;
        }
    }
    public void StartService()
    {
        isRunning = true;
        StartCoroutine(SetPlayerPosition());
    }
    public void StopService()
    {
        isRunning = false;
        StopCoroutine(SetPlayerPosition());
    }

    private IEnumerator SetPlayerPosition()
    {
        while (isRunning)
        {
            yield return new WaitForEndOfFrame();
            viewerPosition = player.position;
            viewerCoordinate = new float2(Mathf.RoundToInt(viewerPosition.x / ChunkConfig.width), Mathf.RoundToInt(viewerPosition.z / ChunkConfig.depth));
        }
    }
    public float3 PlayerPosition()
    {
        if (player == null) // Verifica si player es null antes de acceder a su posición
        {
            return new float3(0, 0, 0); // Retorna una posición predeterminada o maneja la situación como prefieras
        }
        return viewerPosition;
    }
    public float2 PlayerCoordinate()
    {
        if (player == null) // Verifica si viewerPosition es null
        {
            return new float2(0, 0); // Retorna una coordenada predeterminada o maneja la situación como prefieras
        }
        return viewerCoordinate;
    }

    public float MapTemperature()
    {
        return Map[viewerCoordinate].GetTemperature((int)viewerPosition.x, (int)viewerPosition.z);
    }
    public float MapHumidity()
    {
        return Map[viewerCoordinate].GetHumidity((int)viewerPosition.x, (int)viewerPosition.z);
        // Map[coord.x ,coord.y].GetHumidity((int)player.position.x, (int)player.position.z);
    }
    public float MapHeight()
    {
        return Map[viewerCoordinate].GetHeight((int)viewerPosition.x, (int)viewerPosition.z);
    }

    public void PlayerDied()
    {
        throw new System.NotImplementedException();
    }

    public Transform PlayerTransform()
    {
        return player;
    }
}
}