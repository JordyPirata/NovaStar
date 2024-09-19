using Services.Player;
using UnityEngine;
using UI.Hud;
using Services.Interfaces;
using System;
using UnityEngine.SceneManagement;

namespace Services
{
    [Serializable]
    public class HUDHolder : MonoBehaviour, IHUDService
    {
        // TODO: Make game Event System to handle this
        void Awake()
        {
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                switch (scene.name)
                {
                    case SceneLoader.GameScene:
                        // Search for the HUD in the scene
                        healthBar = FindObjectOfType<HealthBar>();
                        staminaBar = FindObjectOfType<StaminaBar>();
                        hungerBar = FindObjectOfType<HungerBar>();
                        thirstBar = FindObjectOfType<ThirstBar>();
                        miniMap = FindObjectOfType<MiniMap>();
                        freezeScreen = FindObjectOfType<FreezeScreen>();
                        break;
                }
            };
        }
        // Restrict the value to be between 0 and 1
        public float HealthValue
        {
            get => healthBar.Value;
            set => healthBar.Value = Mathf.Clamp01(value);
        }        
        public float StaminaValue
        {
            get => staminaBar.Value;
            set => staminaBar.Value =  Mathf.Clamp01(value);
        }
        public float HungerValue
        {
            get => hungerBar.Value;
            set => hungerBar.Value =  Mathf.Clamp01(value);
        }
        public float ThirstValue
        {
            get => thirstBar.Value;
            set => thirstBar.Value =  Mathf.Clamp01(value);
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