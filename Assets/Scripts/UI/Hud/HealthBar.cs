using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

namespace UI
{
public class HealthBar : BaseStatBar
{
    public float Health
    {
        get => Fill.fillAmount;
        set => Fill.fillAmount = value;
}
}
}
