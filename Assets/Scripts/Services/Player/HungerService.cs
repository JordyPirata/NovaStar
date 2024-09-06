using Services.Interfaces;

namespace Services.Player
{
    public class HungerService : StatService, IHungerService
    {
        public int Hunger { get => Stat; set => Stat = value; }
    }
}
