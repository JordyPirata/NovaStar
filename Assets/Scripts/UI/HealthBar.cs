using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

namespace UI
{
public class HealthBar : MonoBehaviour
{
    [SerializeField]private readonly Slider healthBar;

    public float value
    {
        get => healthBar.value;
        set => healthBar.value = value;
    }
}
}
