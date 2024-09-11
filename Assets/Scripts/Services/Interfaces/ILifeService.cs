namespace Services.Interfaces
{
    public interface ILifeService
    {
        int Life { get; set; }
        void StartService();
        void StopService();
        void IncreaseStat(int amount);
    }
}
