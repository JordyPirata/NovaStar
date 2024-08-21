using Services;
using Services.Interfaces;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
public class MiniMap : MonoBehaviour
{
    private IPlayerInfo playerInfo;
    public void Awake()
    {
        playerInfo = ServiceLocator.GetService<IPlayerInfo>();
    }
    public void Update()
    {
        float3 playerPosiotion = playerInfo.GetPlayerPosition();
        
        // Set the Minimap camera
        gameObject.transform.position = new Vector3(playerPosiotion.x, playerPosiotion.y + 10, playerPosiotion.z);
        
    }
}
}