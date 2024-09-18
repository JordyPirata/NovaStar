using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud
{
    public class FreezeScreen : MonoBehaviour
    {
        private Image freezeImage;
        public Material freezeMaterial;
        public float Value
        {
            get => freezeMaterial.GetFloat("_Aperture");
            set => ChangeFreezeScreen(value);
        }

        public void Awake()
        {
            freezeImage = GetComponent<Image>();
            freezeMaterial = freezeImage.material;
        }    
        /// <summary>
        /// Take a normalized value between 0 and 1
        /// </summary>
        public void ChangeFreezeScreen(float value)
        {
            // Change into a normalized value between 0 and .15
            value = Normalize(value, 0, 0.15f);
            freezeMaterial.SetFloat("_Aperture", value);
        }
        private float Normalize(float value, float min, float max)
        {
            // normalize value between 0 and .15
            return value * (max - min) + min;
        }
    }
}