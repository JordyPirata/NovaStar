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
    [SerializeField] private FirstPersonCharacter firstPersonCharacter;
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
        ServiceLocator.Register<IFirstPersonController>(firstPersonCharacter);
    }
    private void UnRegisterServices()
    {
        ServiceLocator.UnRegister<IFirstPersonController>();
    }
}
}