using Services.Interfaces;
using UnityEngine.Events;
public class HydrationService : StatService, IThirstService
{
    public int Hydration { get => Stat; set => Stat = value; }
}
