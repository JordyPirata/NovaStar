namespace Services.Interfaces
{
interface IHydrationService
{
    int Hydration { get; set; }
    void StartService();
    void StopService();
    void IncreaseStat(int amount);
    void DecreaseStat(int amount);
}   
}