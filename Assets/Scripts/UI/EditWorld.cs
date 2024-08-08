using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Services;
using Services.Interfaces;
using Unity.Mathematics;
using Util;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Events;
using System.Linq;

namespace UI
{
public class EditWorld : MonoBehaviour
{
    public Event OnWorldChanged;
    public GameObject createGamePanel;
    public Button backButton;
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
    public void OnEnable()
    {
        state = new()
        {
            seed = worldData.GetSeed(),
            width = 200,
            height = 200,
            coords = new float2(0, 0),
            temperatureRange = worldData.GetTemperatureRange(),
            humidityRange = worldData.GetHumidityRange()
        };

        AddListeners(); // Add listeners to the UI elements
        // Convert the seed to string
        InputSeed.text = worldData.GetSeed().ToString();

        // Set world name
        worldName.text = worldData.GetName();
        
        // Set the sliders
        StartCoroutine(SetSliders());
    }
    public void OnDisable()
    {
        // Remove the listeners
        InputSeed.onValueChanged.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();
        temperatureSlider.OnValueChanged.RemoveAllListeners();
        humiditySlider.OnValueChanged.RemoveAllListeners();
    }
    public IEnumerator SetSliders()
    {
        yield return new WaitForSeconds(1);
        
        temperatureSlider.MinValue = worldData.GetTemperatureRange().x;
        temperatureSlider.MaxValue = worldData.GetTemperatureRange().y;
        humiditySlider.MinValue = worldData.GetHumidityRange().x;
        humiditySlider.MaxValue = worldData.GetHumidityRange().y;
        Debug.Log("SetSliders Temperature: "  + worldData.GetTemperatureRange() + " Temperature: " + worldData.GetHumidityRange());
    }
    private void AddListeners()
    {
        // Add the listener to the sliders
        InputSeed.onValueChanged.AddListener(OnSeedValueChanged);

        // Back button event
        backButton.onClick.AddListener(() => {
            createGamePanel.SetActive(true);
            gameObject.SetActive(false);
            worldData.UpdateWorld();
        });

        // Add the listener to the sliders
        temperatureSlider.OnValueChanged.AddListener(OnTemperatureChanged);
        humiditySlider.OnValueChanged.AddListener(OnHumidityChanged);
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
            GenerateImage();
        }
        else
        {
            // La conversión falló, maneja el error adecuadamente
            Debug.LogError($"El valor ingresado '{seed}' no es un número válido.");
        }
    }
    private void OnTemperatureChanged(float x , float y)
    {
        int2 intT = new((int)x, (int)y);
        state.temperatureRange = intT;
        worldData.SetTemperatureRange(intT);
        GenerateImage();
    }

    private void OnHumidityChanged(float x, float y)
    {
        int2 intH = new((int)x, (int)y);
        state.humidityRange = intH;
        worldData.SetHumidityRange(intH);
        GenerateImage();
    }

    private void GenerateImage()
    {
        newTexture = textureMapGen.GenerateTextureMap(state);
        newTexture.Apply();
        // Cambia la imagen del panel.
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