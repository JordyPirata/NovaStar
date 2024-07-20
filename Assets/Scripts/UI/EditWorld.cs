using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Services;
using Services.Interfaces;
using Unity.Mathematics;
using Util;
using Unity.VisualScripting;

namespace UI
{
    public class EditWorld : MonoBehaviour
{
    public DoubleSlider temperatureSlider;
    public DoubleSlider humiditySlider;
    TMP_InputField Seed;
    Image image;

    public GameObject Panel;
    private Texture2D newTexture;

    public void Awake()
    {
        image = Panel.GetComponent<Image>();
    }

    public void Start()
    {
        TextureMapState state = new()
        {
            seed = 0,
            width = 200,
            height = 200,
            coords = new float2(0, 0),
            temperatureRange = new int2(-10, 30),
            humidityRange = new int2(0, 400)
        };
        newTexture = ServiceLocator.GetService<ITextureMapGen>().GenerateTextureMap(state);
        newTexture.Apply();
        EventHandler handler = new(temperatureSlider.OnValueChanged, humiditySlider.OnValueChanged);
        handler.OnValueChanged.AddListener(OnValueChanged);
        ChangePanelImage();
    }

    private void OnValueChanged(float2 t, float2 h)
    {
        Debug.Log("Temperature: " + math.int2(t) + " Humidity: " + math.int2(h));
        TextureMapState state = new()
        {
            seed = 0,
            width = 200,
            height = 200,
            coords = new float2(0, 0),
            temperatureRange = math.int2(t),
            humidityRange = math.int2(h)
        };
        newTexture = ServiceLocator.GetService<ITextureMapGen>().GenerateTextureMap(state);
        newTexture.Apply();
        ChangePanelImage();
    }

    // Método para cambiar la imagen del panel utilizando una Texture2D.
    private void ChangePanelImage()
    {
        if (image != null && newTexture != null)
        {
            // Convierte la Texture2D a Sprite.
            Sprite newSprite = Sprite.Create(newTexture, new Rect(0.0f, 0.0f, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
            
            image.sprite = newSprite; // Cambia la imagen del panel.
        }
    }

    public void PlayGame()
    {
        ServiceLocator.GetService<ISceneLoader>().LoadScene(ISceneLoader.Game);
    }
}
}