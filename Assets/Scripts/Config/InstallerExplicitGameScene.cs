using System;
using Services;
using Services.PlayerPath;
using UnityEditor.Localization.Plugins.XLIFF.V20;
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