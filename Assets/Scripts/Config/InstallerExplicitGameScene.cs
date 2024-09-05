using System;
using Services;
using UnityEngine;

namespace Config
{
    public class InstallerExplicitGameScene : MonoBehaviour
    {
        [SerializeField] private SceneReferences sceneReferences;

        private void Awake()
        {
            ServiceLocator.Register<ISceneReferences>(sceneReferences);
        }
    }
}