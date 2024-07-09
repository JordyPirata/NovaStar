using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Services;
using Unity.Mathematics;


namespace UI
{
public class EditWorld : MonoBehaviour
{
    public DoubleSlider temperatureSlider;
    public DoubleSlider humiditySlider;
    TMP_InputField Seed;
    Image image;

    public GameObject Panel;
    public Texture2D newTexture;

    public void Awake()
    {
        image = Panel.GetComponent<Image>();
    }

    public void Start()
    {
        newTexture = ServiceLocator.GetService<ITextureMapGen>().GenerateTextureMap(new float2(0, 0), 300, 300);
        temperatureSlider.OnValueChanged.AddListener(OnTemperatureChanged);
        humiditySlider.OnValueChanged.AddListener(OnHumidityChanged);
        ChangePanelImage();
    }

    private void OnHumidityChanged(float arg0, float arg1)
    {
        
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