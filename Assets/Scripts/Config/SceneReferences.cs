using Services.PlayerPath;
using UnityEngine;

namespace Config
{
    public class SceneReferences : MonoBehaviour, ISceneReferences
    {
        [SerializeField] private PlayerMediator playerMediator;
        public PlayerMediator GetPlayerMediator()
        {
            return playerMediator;
        }
    }
}