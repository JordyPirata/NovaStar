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
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private StaminaBar _staminaBar;
        [SerializeField] private HungerBar _hungerBar;
        [SerializeField] private ThirstBar _thirstBar;
        [SerializeField] private MiniMap _miniMap;
        [SerializeField] private FreezeScreen _freezeScreen;

        // Restrict the value to be between 0 and 1
        public float HealthValue
        {
            get => HealthBar != null ? HealthBar.Value : 0f;
            set
            {
                if (HealthBar != null)
                {
                    HealthBar.Value = Mathf.Clamp01(value);
                }
            }
        }        
        public float StaminaValue
        {
            get => StaminaBar != null ? StaminaBar.Value : 0f;
            set
            {
                if (StaminaBar != null)
                {
                    StaminaBar.Value = Mathf.Clamp01(value);
                }
            }
        }
        public float HungerValue
        {
            get => HungerBar != null ? HungerBar.Value : 0f;
            set
            {
                if (HungerBar != null)
                {
                    HungerBar.Value = Mathf.Clamp01(value);
                }
            }
        }
        public float ThirstValue
        {
            get => ThirstBar != null ? ThirstBar.Value : 0f;
            set
            {
                if (ThirstBar != null)
                {
                    ThirstBar.Value = Mathf.Clamp01(value);
                }
            }
        }
        public float FreezeScreenValue
        {
            get => FreezeScreen.Value;
            set => FreezeScreen.Value =  Mathf.Clamp01(value);
        }

        public HealthBar HealthBar 
        { 
            get => _healthBar; 
            set => _healthBar = value;
        }
        public StaminaBar StaminaBar
        {
            get => _staminaBar;
            set => _staminaBar = value;
        }
        public HungerBar HungerBar
        {
            get => _hungerBar;
            set => _hungerBar = value;
        }
        public ThirstBar ThirstBar 
        {
            get => _thirstBar;
            set => _thirstBar = value;
        }
        public MiniMap MiniMap 
        {
            get => _miniMap;
            set => _miniMap = value;
        }
        public FreezeScreen FreezeScreen 
        {
            get => _freezeScreen;
            set => _freezeScreen = value;
        }

    }
}