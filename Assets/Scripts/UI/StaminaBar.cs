using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
public class StaminaBar : MonoBehaviour
{
    Image staminaBar;
    void Awake()
    {
        staminaBar = GetComponent<Image>();
    }
    /// <summary>
    /// Take a normalized value between 0 and 1
    /// </summary>
    public void FillStamina(float amount)
    {
        staminaBar.fillAmount = amount;
    }

    public void EmptyStamina()
    {
        staminaBar.fillAmount = 0;
    }
    public void FullStamina()
    {
        staminaBar.fillAmount = 1;
    }

}
}