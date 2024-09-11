using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FreezeScreen : MonoBehaviour
    {
        private Image freezeImage;
        public Material freezeMaterial;

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
            value = Mathf.Clamp(value, 0f, 1f);
            freezeMaterial.SetFloat("_Aperture", value);
        }
    }
}