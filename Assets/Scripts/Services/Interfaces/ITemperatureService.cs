namespace Services.Interfaces
{
    interface ITemperatureService
    {
        int Temperature { get; set; }
        void StartService();
        void StopService();
        void IncreaseStat(int amount);
        void DecreaseStat(int amount);
    }
}