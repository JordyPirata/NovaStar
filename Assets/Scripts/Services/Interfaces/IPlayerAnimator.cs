using UnityEngine;

namespace Services.Interfaces
{
    public interface IPlayerAnimator
    {
        void PlayerWalking(Vector3 velocity);
        void PlayerJump();
        void PlayerGliding(bool planning);
    }
}