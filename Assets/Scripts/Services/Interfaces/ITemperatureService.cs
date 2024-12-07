namespace Services.Interfaces
{
    interface ITemperatureService
    {
        int Temperature { get; set; }
        void IncreaseStat(int amount);
        void DecreaseStat(int amount);
    }
}