namespace Services.Interfaces
{
    public interface ITimeService
    {
        string GetTime();
        void SpendTime(float minutesInBike);
        void StartRunningTime();
    }
}