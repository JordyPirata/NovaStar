using Services.Interfaces;
public class HydrationService : StatService, IHydrationService
{
    public int Hydration { get => Stat; set => Stat = value; }
}
