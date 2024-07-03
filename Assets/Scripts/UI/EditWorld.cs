using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace UI
{
public class EditWorld : MonoBehaviour
{
    public DoubleSlider temperatureSlider;
    public DoubleSlider humiditySlider;
    TMP_InputField Seed;
    Image image;

    public GameObject Panel;
    public Texture2D newTexture; // Referencia pública para asignar desde el editor de Unity.

    void Awake()
    {
        image = Panel.GetComponent<Image>();
    }

    void Start()
    {
        temperatureSlider.OnValueChanged.AddListener(OnTemperatureChanged);
        humiditySlider.OnValueChanged.AddListener(OnHumidityChanged);
    }

    private void OnHumidityChanged(float arg0, float arg1)
    {
        ChangePanelImage();
    }

    private void OnTemperatureChanged(float arg0, float arg1)
    {
        // Implementación pendiente.
    }

    // Método para cambiar la imagen del panel utilizando una Texture2D.
    public void ChangePanelImage()
    {
        if (image != null && newTexture != null)
        {
            // Convierte la Texture2D a Sprite.
            Sprite newSprite = Sprite.Create(newTexture, new Rect(0.0f, 0.0f, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
            
            image.sprite = newSprite; // Cambia la imagen del panel.
        }
    }
}
}