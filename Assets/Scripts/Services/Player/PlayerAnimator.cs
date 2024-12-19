using System;
using Services.Interfaces;
using UnityEngine;

namespace Services.Player
{
    public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
    {
        [SerializeField] private Animator playerAnimator;


        public void PlayerWalking(Vector3 velocity)
        {
            playerAnimator.SetFloat("Walking", velocity.magnitude);
        }

        public void PlayerJump()
        {
            playerAnimator.SetTrigger("Jump");
        }

        public void PlayerGliding(bool gliding)
        {
            playerAnimator.SetBool("Gliding", gliding);
        }
    }
}