using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud
{
    public abstract class BaseStatBar : MonoBehaviour
    {
        public float Value
        {
            get => Fill.fillAmount;
            set => Fill.fillAmount = value;
        }
        public Image Fill;
        /// <summary>
        /// Take a normalized value between 0 and 1
        /// </summary>
        public void AddAmount(float amount)
        {
            amount = Mathf.Clamp(amount, 0f, 1f);
            Fill.fillAmount += amount;
        }
        /// <summary>
        /// Take a normalized value between 0 and 1
        /// </summary>
        public void SubtractAmount(float amount)
        {
            amount = Mathf.Clamp(amount, 0f, 1f);
            Fill.fillAmount -= amount;
        }
        public void EmptyStat()
        {
            Fill.fillAmount = 0;
        }
        public void FullStat()
        {
            Fill.fillAmount = 1;
        }
    }
}