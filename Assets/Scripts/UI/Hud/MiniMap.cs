using Services;
using Services.Interfaces;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.Hud
{
public class MiniMap : MonoBehaviour
{
    [SerializeField] GameObject minimapPanel;
    private IPlayerInfo playerInfo;
    public void Start()
    {
        playerInfo = ServiceLocator.GetService<IPlayerInfo>();
    }
    public void Update()
    {
        float3 playerPosiotion = playerInfo.PlayerPosition();
        
        // Set the Minimap camera
        gameObject.transform.position = new Vector3(playerPosiotion.x, playerPosiotion.y + 10, playerPosiotion.z);
    }
}
}