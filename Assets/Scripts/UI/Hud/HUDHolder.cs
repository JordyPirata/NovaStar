using Services.Player;
using UnityEngine;

namespace UI
{

    public class HUDHolder : MonoBehaviour
    {
        // Editor Slider
        [Range(0, 1)]
        public float Value;

        // On Change Slider in Editor
        public void OnValidate()
        {
            healthBar.Value = Value;
            staminaBar.Value = Value;
            hungerBar.Value = Value;
            thirstBar.Value = Value;
            freezeScreen.Value = Value;
        }

        public HealthBar healthBar;
        public StaminaBar staminaBar;
        public HungerBar hungerBar;
        public ThirstBar thirstBar;
        public MiniMap miniMap;
        public FreezeScreen freezeScreen;

    }
}