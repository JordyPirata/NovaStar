using Services.Player;

namespace Services.Interfaces
{
    public interface ITemperatureService
    {
        int Temperature { get; set; }
        void IncreaseStat(int amount);
        void DecreaseStat(int amount);
        void MapLoaded();
        void DrinkSomeWater();
        void EquipHat();
        ITemperatureService Configure(IPlayerMediator playerMediator);
        void EquipCoat(bool equip);
    }
}