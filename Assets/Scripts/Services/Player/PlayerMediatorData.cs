using UnityEngine;

namespace Services.Player
{
    [CreateAssetMenu (fileName = "PlayerMediatorData", menuName = "NovaStar/PlayerMediatorData")]
    public class PlayerMediatorData : ScriptableObject
    {
        [SerializeField] public float interactionDistance;
        [SerializeField] public LayerMask interactionLayer;
    }
}