using Services.Interfaces;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerInfoHolder : MonoBehaviour, IPlayerInfoHolder
    {
        public TMP_Text ChunkCoordinate { get; set ; }
        public TMP_Text temperatureText { get; set; }
        public TMP_Text humidityText { get; set; }
        public TMP_Text heightText { get; set; }
        public TMP_Text playerCoordinate { get; set; }
    }
}