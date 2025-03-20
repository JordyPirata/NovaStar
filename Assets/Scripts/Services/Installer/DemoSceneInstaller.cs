using Player.Gameplay.Items;
using Services.Interfaces;
using Services.Player;
using Services.Splatmap;
using Services.WorldGenerator;
using UnityEngine;

namespace Services.Installer
{
public class DemoSceneInstaller : MonoBehaviour
{

    private void Awake()
    {
        RegisterServices();
    }
    private void OnDestroy()
    {
        UnRegisterServices();
    }

    private void RegisterServices()
    {

    }
    private void UnRegisterServices()
    {

    }
}
}