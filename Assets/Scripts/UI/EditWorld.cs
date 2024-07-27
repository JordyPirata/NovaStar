using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Services;
using Services.Interfaces;
using Unity.Mathematics;
using Util;
using System.Collections;
using Unity.VisualScripting;

namespace UI
{
public class EditWorld : MonoBehaviour
{
    public DoubleSlider temperatureSlider;
    public DoubleSlider humiditySlider;
    public TMP_Text worldName;
    public TMP_InputField InputSeed;
    public Image image;
    private Texture2D newTexture;
    private TextureMapState state;
    private IWorldData worldData;
    private ITextureMapGen textureMapGen;
    public void Awake()
    {
        // Get the services
        worldData = ServiceLocator.GetService<IWorldData>();
        textureMapGen = ServiceLocator.GetService<ITextureMapGen>();        
    }
    public void Start()
    {
        StartCoroutine(AddListeners());
    }
    public void InitializeWorld()
    {
        // Convert the seed to string
        InputSeed.text = worldData.GetSeed().ToString();

        // Set world name
        worldName.text = worldData.GetName();

        StartCoroutine(SetSliders());

        // Set the TextureMapState
        state = new()
        {
            seed = worldData.GetSeed(),
            width = 200,
            height = 200,
            coords = new float2(0, 0),
            temperatureRange = math.int2(new float2(temperatureSlider.MinValue, temperatureSlider.MaxValue)),
            humidityRange = math.int2(new float2(humiditySlider.MinValue, humiditySlider.MaxValue))
        };

        // Generate the texture map
        StartCoroutine(GenerateImage());
    }
    private IEnumerator AddListeners()
    {
        yield return new WaitForSeconds(0.03f);
        // Add the listener to the sliders
        InputSeed.onValueChanged.AddListener(OnSeedValueChanged);
        EventHandler handler = new(temperatureSlider.OnValueChanged, humiditySlider.OnValueChanged);
        handler.OnValueChanged.AddListener(OnSlidersValueChanged);
    }
    private IEnumerator SetSliders()
    {
        yield return new WaitForSeconds(0.05f);
        // Set the sliders
        temperatureSlider.MinValue = worldData.GetTemperatureRange().x;
        temperatureSlider.MaxValue = worldData.GetTemperatureRange().y;
        humiditySlider.MinValue = worldData.GetHumidityRange().x;
        humiditySlider.MaxValue = worldData.GetHumidityRange().y;
    }

    public void OnSeedValueChanged(string seed)
    {
        if (string.IsNullOrEmpty(seed))
        {
            return;
        }
        //string to int
        
        if (int.TryParse(seed, out int seedValue))
        {
            // La conversión fue exitosa, ahora puedes usar seedValue
            Debug.Log($"El valor ingresado '{seed}' es un número válido: {seedValue}");
            worldData.SetSeed(seedValue);
            state.seed = seedValue;
            StartCoroutine(GenerateImage());
        }
        else
        {
            // La conversión falló, maneja el error adecuadamente
            Debug.LogError($"El valor ingresado '{seed}' no es un número válido.");
        }
    }
    private void OnSlidersValueChanged(float2 t, float2 h)
    {
        int2 intT = math.int2(t), intH = math.int2(h);
        Debug.Log($"Temperature: {intT.x} - {intT.y}, Humidity: {intH.x} - {intH.y}");
        state.temperatureRange = intT;
        worldData.SetTemperatureRange(intT);
        state.humidityRange = intH;
        worldData.SetHumidityRange(intH);
        StartCoroutine(GenerateImage());
    }

    IEnumerator GenerateImage()
    {
        yield return 
        newTexture = textureMapGen.GenerateTextureMap(state);
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