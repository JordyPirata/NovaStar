using Services.Player;
using UnityEngine;
using UI.Hud;
using Services.Interfaces;
using System;
using UnityEngine.SceneManagement;

namespace Services
{
    public class HUDHolder : MonoBehaviour, IHUDService 
    {
        public void Initialize()
        {
            try
            {
                // Initialize the HUD
                healthBar = FindObjectOfType<HealthBar>();
                staminaBar = FindObjectOfType<StaminaBar>();
                hungerBar = FindObjectOfType<HungerBar>();
                thirstBar = FindObjectOfType<ThirstBar>();
                miniMap = FindObjectOfType<MiniMap>();
                freezeScreen = FindObjectOfType<FreezeScreen>();

                // Check if any of the components are null
                if (healthBar == null || staminaBar == null || hungerBar == null || thirstBar == null || miniMap == null || freezeScreen == null)
                {
                    throw new NullReferenceException("One or more HUD components are missing.");
                }
            }
            catch (NullReferenceException ex)
            {
                Debug.LogError($"Initialization failed: {ex.Message}");
                // Handle the exception (e.g., disable the HUD, show an error message, etc.)
            }
        }

        // Restrict the value to be between 0 and 1
        public float HealthValue
        {
            get => healthBar != null ? healthBar.Value : 0f;
            set
            {
                if (healthBar != null)
                {
                    healthBar.Value = Mathf.Clamp01(value);
                }
            }
        }        
        public float StaminaValue
        {
            get => staminaBar != null ? staminaBar.Value : 0f;
            set
            {
                if (staminaBar != null)
                {
                    staminaBar.Value = Mathf.Clamp01(value);
                }
            }
        }
        public float HungerValue
        {
            get => hungerBar != null ? hungerBar.Value : 0f;
            set
            {
                if (hungerBar != null)
                {
                    hungerBar.Value = Mathf.Clamp01(value);
                }
            }
        }
        public float ThirstValue
        {
            get => thirstBar != null ? thirstBar.Value : 0f;
            set
            {
                if (thirstBar != null)
                {
                    thirstBar.Value = Mathf.Clamp01(value);
                }
            }
        }
        public float FreezeScreenValue
        {
            get => freezeScreen.Value;
            set => freezeScreen.Value =  Mathf.Clamp01(value);
        }

        public HealthBar healthBar { get; set; }
        public StaminaBar staminaBar { get; set; }  
        public HungerBar hungerBar  { get; set; }
        public ThirstBar thirstBar { get; set; }
        public MiniMap miniMap { get; set; }
        public FreezeScreen freezeScreen { get; set; }

    }
}