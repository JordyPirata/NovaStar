using Services.Interfaces;
namespace Services.Player
{
public class TemperatureService : StatService, ITemperatureService
{
    public int Temperature { get => Stat; set => Stat = value; }
}
}
