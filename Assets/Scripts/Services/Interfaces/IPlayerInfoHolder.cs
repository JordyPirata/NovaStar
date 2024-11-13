using TMPro;

namespace Services.Interfaces
{
    public interface IPlayerInfoHolder
    {
        public TMP_Text ChunkCoordinate { get; set; }
        public TMP_Text temperatureText { get; set; }
        public TMP_Text humidityText { get; set; }
        public TMP_Text heightText { get; set; }
        public TMP_Text playerCoordinate { get; set; }
    }
}