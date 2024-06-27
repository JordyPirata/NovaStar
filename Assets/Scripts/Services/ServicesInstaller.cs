using Services.Interfaces;
using Services.Repository;
using UnityEngine;

namespace Services
{
    public class ServiceInstaller : MonoBehaviour
    {
        public void Awake()
        {
            // DontDestroyOnLoad(gameObject)
            DontDestroyOnLoad(this);
            InstallServices();
        }
        public void InstallServices()
        {
            ServiceLocator.Register<ISettingsService>(gameObject.AddComponent<SettingsService>());
            ServiceLocator.Register<IPlayerInfo>(gameObject.AddComponent<PlayerInfo>());
            ServiceLocator.Register<IWeldMap>(gameObject.AddComponent<WeldMap>()); 
            ServiceLocator.Register<IMapGenerator>(new MapGenerator());
            ServiceLocator.Register<IRepository>(new GameRepository());
            //Last revision
            ServiceLocator.Register<ISceneLoader>(gameObject.AddComponent<SceneLoader>());
            ServiceLocator.Register<ICreateWorld>(new WorldDataGen());
            ServiceLocator.Register<IWorldData>(gameObject.AddComponent<WorldData>());
        }
    }
}