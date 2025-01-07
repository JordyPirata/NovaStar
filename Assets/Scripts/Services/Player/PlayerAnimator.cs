using System;
using Services.Installer;
using Services.Interfaces;
using UnityEngine;

namespace Services.Player
{
    public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
    {
        [SerializeField] private Animator playerAnimator;

        private void OnEnable()
        {
            EventManager.OnMapLoaded += OnMapLoaded;
        }

        private void OnMapLoaded()
        {
            playerAnimator.SetTrigger("Respawn");
        }

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
        
        private void OnDisable()
        {
            EventManager.OnMapLoaded -= OnMapLoaded;
        }
    }
}