using Unity.Mathematics;
using UnityEngine.Events;

namespace Util
{
    public class EventHandler
    {
        public UnityEvent<float2, float2> OnValueChanged;
        public float2 lastTemperature = new(-10, 30);
        public float2 lastHumidity = new(0, 400);
        public EventHandler(UnityEvent<float, float> temperatureChanged, UnityEvent<float, float> humidityChanged)
        {
            temperatureChanged.AddListener(OnTemperatureChanged);
            humidityChanged.AddListener(OnHumidityChanged);
        }
        private void OnTemperatureChanged(float t0, float t1)
        {
            lastTemperature = new float2(t0, t1);
            // Invocar con un valor específico para identificar el evento de temperatura
            OnValueChanged.Invoke(lastTemperature, lastHumidity);
        }

        private void OnHumidityChanged(float h0, float h1)
        {
            // Invocar con un valor específico para identificar el evento de humedad
            lastHumidity = new float2(h0, h1);
            OnValueChanged.Invoke(lastTemperature, lastHumidity);
        }
    }
}