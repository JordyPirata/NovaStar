using UnityEngine;

namespace Services.Interfaces
{
    public interface IPlayerAnimator
    {
        void PlayerWalking(float velocity);
        void PlayerJump();
        void PlayerGliding(bool planning);
    }
}