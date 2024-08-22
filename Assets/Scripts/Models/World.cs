using Unity.Mathematics;

namespace Models
{
    [System.Serializable]
    public class World
    {
        public bool IsGenerated;
        public string Name;
        public string WorldPath;
        public string Directory;
        public int seed;
        public int2 temperatureRange;
        public int2 humidityRange;
    }
}