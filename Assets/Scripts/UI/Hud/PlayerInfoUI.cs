using Services.Interfaces;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerInfoUI : MonoBehaviour
    {
        private IPlayerInfo playerInfo;
        public TMP_Text coordinateText;
        public TMP_Text temperatureText;
        public TMP_Text humidityText;
        public TMP_Text heightText;
        
        public void Awake()
        {
            playerInfo = ServiceLocator.GetService<IPlayerInfo>();
        }
        public void Update()
        {

            coordinateText.text = $"Coordinate: {playerInfo.PlayerCoordinate()}";
            temperatureText.text = $"Temperature: {playerInfo.MapTemperature()}";
            humidityText.text = $"Humidity: {playerInfo.MapHumidity()}";
            heightText.text = $"Height: {playerInfo.MapHeight()}";
            
        }

    }
}