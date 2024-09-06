namespace Services.Interfaces
{
    interface IHungerService
    {
        int Hunger { get; set; }
        void StartService();
        void StopService();
        void IncreaseStat(int amount);
        void DecreaseStat(int amount);
    }
}
