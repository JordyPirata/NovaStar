using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DoubleSlider;
using UnityEngine.UI;
using TMPro;
using System;

public class EditWorld : MonoBehaviour
{
    public DoubleSlider temperatureSlider;
    public DoubleSlider humiditySlider;
    TMP_InputField Seed;
    Image image;

    public GameObject Panel;
    void Awake()
    {
        image = Panel.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        temperatureSlider.OnValueChanged.AddListener(OnTemperatureChanged);
        humiditySlider.OnValueChanged.AddListener(OnHumidityChanged);
    }

    private void OnHumidityChanged(float arg0, float arg1)
    {
        throw new NotImplementedException();
    }

    private void OnTemperatureChanged(float arg0, float arg1)
    {
        throw new NotImplementedException();
    }
}
